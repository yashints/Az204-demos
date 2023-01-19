using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Src
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
          webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
          {
            var settings = config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddCommandLine(args)
            .Build();
            config.AddAzureAppConfiguration(options =>
            {
              options.Connect(settings["ConnectionStrings:AppConfig"])
                      .UseFeatureFlags();
            });
          })
        .UseStartup<Startup>());
  }
}
