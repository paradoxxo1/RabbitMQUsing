using System.Text;
using RabbitMQ.Client;

namespace WorkQueues.NewTask;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.HostName = "localhost";
        factory.Port = 5672;
        factory.UserName = "guest";
        factory.Password = "guest";


        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "hello",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
           );

        string message = GetMessage(args);
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


    static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
    }
}
