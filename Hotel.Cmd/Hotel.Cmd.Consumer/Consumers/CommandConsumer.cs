using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CQRS.Core.Consumers;
using CQRS.Core.Infrastructure;
using System.Text.Json;
using Hotel.Query.Infrastructure.Converters;
using CQRS.Core.Commands;

namespace Hotel.Cmd.Commands.Consumers;

public class CommandConsumer : ICommandConsumer
{
    // private readonly IConnectionFactory _connectionFactory;
    private readonly ICommandDispatcher _commandDispatcher;

    public CommandConsumer(
        // IConnectionFactory connectionFactory,
        ICommandDispatcher commandDispatcher
    )
    {
        // _connectionFactory = connectionFactory;
        _commandDispatcher = commandDispatcher;
    }

    public void Consume(string queue)
    {
        // using var connection = _connectionFactory.CreateConnection();
        // using var channel = connection.CreateModel();

        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBIT_HOST")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            System.Console.WriteLine($"{message}");

            var options = new JsonSerializerOptions { Converters = { new CommandJsonConverter() } };
            var command = JsonSerializer.Deserialize<BaseCommand>(message, options);

            _commandDispatcher.SendAsync(command);

            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
