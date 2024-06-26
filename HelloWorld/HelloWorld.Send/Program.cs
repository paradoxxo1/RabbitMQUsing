using System.Text;
using RabbitMQ.Client;

namespace HelloWorld.Send;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Press [enter] to send message");
        Console.ReadLine();

        var factory = new ConnectionFactory();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";

        //factory.Uri = new Uri("amqps://qguyrlsf:F-lLRC0MUmn6vTkSC28B8HmH7hrRt2Oq@rat.rmq2.cloudamqp.com/qguyrlsf");

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
           );

        string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: "hello",
            basicProperties: null,
            body: body);

        Console.WriteLine($" [*] Send {message}");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}


