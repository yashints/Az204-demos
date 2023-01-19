using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;

namespace Receiver
{
    internal class EventHubsReceiver
    {
        private BlobContainerClient _blobContainerClient;
        private EventProcessorClient _eventProcessorClient;
        public EventHubsReceiver(EventHubsInfo info)
        {

            _blobContainerClient = new BlobContainerClient(
                info.StorageConnectionString,
                info.StorageContainerName);

            _eventProcessorClient = new EventProcessorClient(
                _blobContainerClient,
                EventHubConsumerClient.DefaultConsumerGroupName,
                info.EventHubConnectionString,
                info.EventHubName);

            // Register handlers for processing events and handling errors
            _eventProcessorClient.ProcessEventAsync += processEventHandler;
            _eventProcessorClient.ProcessErrorAsync += processErrorHandler;
        }

        public async Task ReceiveEvents()
        {            
            try
            {
                using var cancellationToken = new CancellationTokenSource();
                cancellationToken.CancelAfter(TimeSpan.FromSeconds(30));

                await _eventProcessorClient.StartProcessingAsync(cancellationToken.Token);

                // Wait for 30 seconds for the events to be processed
                await Task.Delay(Timeout.Infinite, cancellationToken.Token);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                // Stop the processing
                await _eventProcessorClient.StopProcessingAsync();
            }
        }

        Task processEventHandler(ProcessEventArgs eventArgs)
        {
            try
            {
                if (eventArgs.CancellationToken.IsCancellationRequested)
                {
                    return Task.CompletedTask;
                }

                string partition = eventArgs.Partition.PartitionId;
                string eventBody = eventArgs.Data.EventBody.ToString();
                Console.WriteLine($"Event {eventBody} from partition { partition }.");                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception occured: {0}", ex.Message);
            }
            return Task.CompletedTask;
        }

        Task processErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            try
            {
                Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
                Console.WriteLine(eventArgs.Exception.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occured: {0}", ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}
