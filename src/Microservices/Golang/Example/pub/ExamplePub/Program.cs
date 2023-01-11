using Dapr.Client;

Console.WriteLine("Hello, World!");

string PUBSUB_NAME = "pubsub";
string TOPIC_NAME = "neworder";

CancellationTokenSource source = new CancellationTokenSource();
CancellationToken cancellationToken = source.Token;
using var client = new DaprClientBuilder().Build();

//Using Dapr SDK to publish a topic
string email = "malik.masis@gmail.com";
string no = "+90123456789";
await client.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, new { no = no, Email = email }, cancellationToken);
Console.WriteLine($"Published data with no: {no} and email: {email}");

//dapr run --app-id pub --app-port 6001 --dapr-http-port 3601 --dapr-grpc-port 60001 --app-ssl dotnet run