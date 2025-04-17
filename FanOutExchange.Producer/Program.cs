using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
await using var connection = await factory.CreateConnectionAsync();
await using var channel = await connection.CreateChannelAsync();


const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

await channel.BasicPublishAsync(exchange: "amq.fanout", routingKey: "key-notification", body: body);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();