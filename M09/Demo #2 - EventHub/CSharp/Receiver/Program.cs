using Microsoft.Extensions.Configuration;
using Receiver;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
EventHubsInfo info = config.GetRequiredSection("EventHubsInfo").Get<EventHubsInfo>();
EventHubsReceiver receiver = new EventHubsReceiver(info);
await receiver.ReceiveEvents();
