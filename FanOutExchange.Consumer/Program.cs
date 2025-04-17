
// fanout exchange cunsumer

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
await using var connection = await factory.CreateConnectionAsync();

await using var channel = await connection.CreateChannelAsync();
// consumer
var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    await Task.Yield();
};

Task ConsumeFun(string queueName)
{
    return channel.BasicConsumeAsync(queue: queueName,
        autoAck: true,
        consumer: consumer);
}


await ConsumeFun("q.fanout1");
await ConsumeFun("q.fanout2");
await ConsumeFun("q.fanout3");
await ConsumeFun("q.fanout4");
await ConsumeFun("q.fanout5");