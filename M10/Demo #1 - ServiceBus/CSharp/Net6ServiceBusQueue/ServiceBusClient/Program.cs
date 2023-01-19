using Azure.Messaging.ServiceBus;
string connectionString = "Endpoint=sb://az204sbdemo.servicebus.windows.net/;SharedAccessKeyName=SendReceive;SharedAccessKey=yl3ob4ymJajXoNBmxws2n7D0frjA9Sj4miXS+dt6ZIU=;EntityPath=basicqueue";
string queueName = "BasicQueue";
// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);

// create the sender
ServiceBusSender sender = client.CreateSender(queueName);

// create a message that we can send. UTF-8 encoding is used when providing a string.
ServiceBusMessage message = new ServiceBusMessage("Hello world!");

// send the message
await sender.SendMessageAsync(message);

// create a receiver that we can use to receive the message
ServiceBusReceiver receiver = client.CreateReceiver(queueName);

// the received message is a different type as it contains some service set properties
//ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();

// get the message body as a string
//string body = receivedMessage.Body.ToString();
//Console.WriteLine(body);
