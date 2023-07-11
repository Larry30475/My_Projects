using System.Text;
using RabbitMQ.Client;

MyData.info();

var factory = new ConnectionFactory { HostName = "10.108.127.208", UserName = "wiktors", Password = "wiktors" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

const string message = "Jan";
var body = Encoding.UTF8.GetBytes(message);

Random rnd = new Random();

for (int i = 0; i < 5; i++)
{
    channel.BasicPublish(exchange: string.Empty,
    routingKey: "hello",
    basicProperties: null,
    body: body);

    var delay = rnd.Next(1, 6);
    await Task.Delay(delay * 1000);
    Console.WriteLine($" [x] Sent {message} with delay {delay}");
}

Console.WriteLine("Press [enter] to exit");
Console.ReadLine();
