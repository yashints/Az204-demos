using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace Sender
{
    internal class EventHubsSender
    {
        private EventHubsInfo _info;
        public EventHubsSender(EventHubsInfo info)
        {
            _info = info;

        }
        public async Task SendEvents()
        {
            await using (var producer = new EventHubBufferedProducerClient(_info.ConnectionString, _info.EventHubName))
            {
                producer.SendEventBatchFailedAsync += args =>
                {
                    Console.WriteLine($"Publishing failed for { args.EventBatch.Count } events.  Error: '{ args.Exception.Message }'");
                    return Task.CompletedTask;
                };

                // The success handler is optional.

                producer.SendEventBatchSucceededAsync += args =>
                {
                    Console.WriteLine($"{ args.EventBatch.Count } events were published to partition: '{ args.PartitionId }.");
                    return Task.CompletedTask;
                };

                await SendMessagesToEventHub(producer, 100);
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static async Task SendMessagesToEventHub(EventHubBufferedProducerClient producer, int numMessagesToSend)
        {
            for (var i = 0; i < numMessagesToSend; i++)
            {
                try
                {
                    var message = $"Message {i}";
                    Console.WriteLine($"Sending message: {message}");
                    await producer.EnqueueEventAsync(new EventData(new BinaryData(message)));
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                }

                await Task.Delay(10);
            }

            Console.WriteLine($"{numMessagesToSend} messages sent.");
        }
    }
}
