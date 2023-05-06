using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Hotel.Cmd.Commands;
using Hotel.Cmd.Commands.Consumers;
using Hotel.Cmd.Domain.Aggregates;
using Hotel.Cmd.Infrastructure.Config;
using Hotel.Cmd.Infrastructure.Dispatchers;
using Hotel.Cmd.Infrastructure.Handlers;
using Hotel.Cmd.Infrastructure.Producers;
using Hotel.Cmd.Infrastructure.Repositories;
using Hotel.Cmd.Infrastructure.Stores;
using Hotel.Query.Infrastructure.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver.Core.Connections;
using RabbitMQ.Client;

string GetEnv(string envName)
{
    string envValue = Environment.GetEnvironmentVariable(envName);

    if (string.IsNullOrEmpty(envValue))
    {
        throw new Exception($"{envName} environment variable is not set.");
    }

    return envValue;
}

// MongoDB Event Store configs
string mongoUser = GetEnv("MONGO_USER");
string mongoPasswd = GetEnv("MONGO_PASSWORD");
string mongoHost = GetEnv("MONGO_HOST");
string mongoPort = GetEnv("MONGO_PORT");
string mongoDb = GetEnv("MONGO_DATABASE");
string mongoColl = GetEnv("MONGO_COLLECTION");

// Kafka configs
string kafkaHost = GetEnv("KAFKA_HOST");
string kafkaPort = GetEnv("KAFKA_PORT");

// RabbitMQ condigs
string rabbitHost = GetEnv("RABBIT_HOST");
string rabbitPort = GetEnv("RABBIT_PORT");

Action<MongoDbConfig> configureMongoDb = (
    o =>
    {
        o.ConnectionString = $"mongodb://{mongoUser}:{mongoPasswd}@{mongoHost}:{mongoPort}";
        o.Collection = mongoColl;
        o.Database = mongoDb;
    }
);

Action<ProducerConfig> configureProducer = (
    o =>
    {
        o.BootstrapServers = $"{kafkaHost}:{kafkaPort}";
    }
);

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.Configure<MongoDbConfig>(configureMongoDb);
    services.Configure<ProducerConfig>(configureProducer);

    services.AddScoped<IEventStoreRepository, EventStoreRepository>();
    services.AddScoped<IEventProducer, EventProducer>();
    services.AddScoped<IEventStore, EventStore>();
    services.AddScoped<IEventSourcingHandler<ReservationAggregate>, EventSourcingHandler>();
    services.AddScoped<ICommandHandler, CommandHandler>();
    // services.AddSingleton<RabbitMQ.Client.ConnectionFactory>(
    //     new RabbitMQ.Client.ConnectionFactory { HostName = rabbitHost, Port = int.Parse(rabbitPort) }
    // );

    // register command handler methods
    var commandHandler = services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
    var dispatcher = new CommandDispatcher();
    dispatcher.RegisterHandler<CreateReservationCommand>(commandHandler.HandleAsync);
    dispatcher.RegisterHandler<EditReservationCommand>(commandHandler.HandleAsync);
    dispatcher.RegisterHandler<CancelReservationCommand>(commandHandler.HandleAsync);
    services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

    services.AddScoped<ICommandConsumer, CommandConsumer>();

    services.AddHostedService<CommandConsumerHostedService>();
});

using var host = builder.Build();

host.Run();
