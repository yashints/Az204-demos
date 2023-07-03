
using Microsoft.Extensions.Configuration;
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .AddEnvironmentVariables()
    .Build();

StorageQueueInfo info = config.GetRequiredSection("StorageQueueInfo").Get<StorageQueueInfo>();


QueueClient queueClient = new QueueClient(info.ConnectionString, info.QueueName);

queueClient.CreateIfNotExists();

if (queueClient.Exists())
{
    Console.WriteLine($"Queue created: '{queueClient.Name}'");
}
else
{
    Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
}

queueClient.SendMessage("Hello from the console app");

PeekedMessage[] peekedMessage = queueClient.PeekMessages();

Console.WriteLine($"Peeked message: '{peekedMessage[0].Body}'");