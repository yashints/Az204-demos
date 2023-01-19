// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace SampleEphReceiver
{
  using System;
  using System.Threading.Tasks;
  using Microsoft.Azure.EventHubs;
  using Microsoft.Azure.EventHubs.Processor;

  public class Program
  {
    private const string EventHubConnectionString = "Endpoint=sb://az204ehubsdemo.servicebus.windows.net/;SharedAccessKeyName=ReadOnly;SharedAccessKey=hIpyyuAQls+u5Mig24ZtQQ9zAQTzlZv9EZmDhrXlQoY=;EntityPath=demoeventhub";
    private const string EventHubName = "DemoEventHub";
    private const string StorageContainerName = "messages";
    private const string StorageAccountName = "az20495f2";
    private const string StorageAccountKey = "l/KBIkReIHno8P1oRY/26CLfxsfafIk2E3laOyvjDRc7EQiImMKKcvSD8oBWmjX72CCU9ngNVn3n+AStLJbXpg==";

    private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

    public static void Main(string[] args)
    {
      MainAsync(args).GetAwaiter().GetResult();
    }

    private static async Task MainAsync(string[] args)
    {
      Console.WriteLine("Registering EventProcessor...");

      var eventProcessorHost = new EventProcessorHost(
          EventHubName,
          PartitionReceiver.DefaultConsumerGroupName,
          EventHubConnectionString,
          StorageConnectionString,
          StorageContainerName);

      // Registers the Event Processor Host and starts receiving messages
      await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

      Console.WriteLine("Receiving. Press enter key to stop worker.");
      Console.ReadLine();

      // Disposes of the Event Processor Host
      await eventProcessorHost.UnregisterEventProcessorAsync();
    }
  }
}
