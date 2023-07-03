using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables()
    .Build();

ServiceBusConfig? sbConfig = config.GetRequiredSection("ServiceBusConfig").Get<ServiceBusConfig>();
if (sbConfig is null || string.IsNullOrWhiteSpace(sbConfig.ConnectionString) || string.IsNullOrWhiteSpace(sbConfig.QueueName))
{
  Console.WriteLine("ServiceBusConfig is null");
  return;
}

// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(sbConfig.ConnectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(sbConfig.QueueName);

// create a message that we can send. UTF-8 encoding is used when providing a string.
ServiceBusMessage message = new ServiceBusMessage("Hello world!");

// send the message
message.ApplicationProperties.Add("Custom Field", "AZ204");
//await sender.SendMessageAsync(message);


// create a receiver that we can use to receive the message
ServiceBusReceiver receiver =
  client.CreateReceiver(
    sbConfig.QueueName,
    new ServiceBusReceiverOptions() { ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete }
  );

// the received message is a different type as it contains some service set properties
ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

// get the message body as a string
string body = receivedMessage.Body.ToString();
Console.WriteLine(body);
