namespace CosmosDemo
{
  class Program
  {
    IConfiguration config = new ConfigurationBuilder()
     .AddJsonFile("appsettings.json")
     .AddEnvironmentVariables()
     .Build();

    Settings settings = config.GetRequiredSection("Settings").Get<Settings>();

  }
}



