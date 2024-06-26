# RabbitMQ Using

This repository contains a comprehensive RabbitMQ course that covers the following topics:

- **Basic Concepts**: Understanding the fundamentals of RabbitMQ and message queuing systems.
- **Message Queuing Systems**: Overview of how message queuing systems work and the role of RabbitMQ within these systems.
- **Installation and Configuration**: Steps to install and configure RabbitMQ for daily use.
- **Queuing Models**: Detailed exploration of Direct, Topic, Fanout, and Headers message queuing models and their use cases.
- **Advanced Features**: Utilizing advanced features, performance optimizations, and best practices for RabbitMQ.
- **Publish-Subscribe Pattern**: Implementing the publish-subscribe pattern with RabbitMQ.
- **Routing**: Techniques for message routing in RabbitMQ.
- **Topics**: Using topic exchanges for flexible routing.
- **Work Queues**: Managing task distribution with work queues.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [RabbitMQ Server](https://www.rabbitmq.com/download.html)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/rabbitmq-course.git
   cd rabbitmq-course
   ```

2. Install RabbitMQ and start the server. For detailed instructions, refer to the [RabbitMQ installation guide](https://www.rabbitmq.com/download.html).

### Usage

#### Sender Example

This example demonstrates how to send a message to a RabbitMQ queue.

```csharp
using System;
using System.Text;
using RabbitMQ.Client;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(" Press [enter] to send message");
        Console.ReadLine();

        var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

        string message = "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: body);

        Console.WriteLine($" [*] Sent {message}");

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
```

#### Receiver Example

This example demonstrates how to receive a message from a RabbitMQ queue.

```csharp
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

        Console.WriteLine(" [*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };

        channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit");
        Console.ReadLine();
    }
}
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

For more information, visit the [official RabbitMQ documentation](https://www.rabbitmq.com/documentation.html).
