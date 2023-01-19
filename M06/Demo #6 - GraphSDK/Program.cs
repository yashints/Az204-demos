using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System.Text.Json;
using Azure.Identity;

namespace GraphClient
{
  class Program
  {
    static async Task Main(string[] args)
    {
      AuthenticationConfig config = AuthenticationConfig.ReadFromJsonFile("appsettings.json");

      var graphScopes = new string[] { $"{config.ApiUrl}.default" };

      var options = new TokenCredentialOptions
      {
        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
      };

      var clientSecretCredential = new ClientSecretCredential(
          config.Tenant, config.ClientId, config.ClientSecret, options);

      var graphClient = new GraphServiceClient(clientSecretCredential, graphScopes);

      var users = await graphClient
          .Users
          .Request()
          .GetAsync();

      foreach (var user in users)
      {
        Console.WriteLine(user.DisplayName);
      }

    }
  }
}
