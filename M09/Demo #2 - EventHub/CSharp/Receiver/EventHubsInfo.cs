namespace Receiver
{
    internal class EventHubsInfo
    {
        public string EventHubConnectionString { get; set; }
        public string EventHubName { get; set; }
        public string StorageContainerName { get; set; }
        public string StorageConnectionString { get; set; }
    }
}
