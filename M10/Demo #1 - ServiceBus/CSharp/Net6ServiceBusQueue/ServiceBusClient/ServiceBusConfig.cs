internal class ServiceBusConfig
{
  //make this property nullable so we can check if it was set
  public string? ConnectionString { get; set; }
  public string? QueueName { get; set; }
}