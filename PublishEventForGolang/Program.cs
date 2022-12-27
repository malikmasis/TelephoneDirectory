// See https://aka.ms/new-console-template for more information
using Dapr.Client;

Console.WriteLine("Hello, World!");

string PUBSUB_NAME = "messages";
string TOPIC_NAME = "neworder";
while (true)
{
    System.Threading.Thread.Sleep(2000);
    Random random = new Random();
    int orderId = random.Next(1, 1000);
    CancellationTokenSource source = new CancellationTokenSource();
    CancellationToken cancellationToken = source.Token;
    using var client = new DaprClientBuilder().Build();
    //Using Dapr SDK to publish a topic
    await client.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, orderId, cancellationToken);
    Console.WriteLine("Published data: " + orderId);
}