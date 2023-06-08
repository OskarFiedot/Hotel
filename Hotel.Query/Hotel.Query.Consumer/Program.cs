using Hotel.Query.Infrastructure.DataAccess;
using Hotel.Query.Infrastructure.Repositories;
using Hotel.Query.Infrastructure.Handlers;
using Microsoft.EntityFrameworkCore;
using Confluent.Kafka;
using Hotel.Query.Infrastructure.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Hotel.Query.Domain.Repositories;
using CQRS.Core.Consumers;
using Hotel.Query.Consumer.Queries;
using Hotel.Query.Consumer.Consumers;

string GetEnv(string envName)
{
    string envValue = Environment.GetEnvironmentVariable(envName);

    if (string.IsNullOrEmpty(envValue))
    {
        throw new Exception($"{envName} environment variable is not set.");
    }

    return envValue;
}

// PostgreSQL config
string postgresHost = GetEnv("POSTGRES_HOST");
string postgresUser = GetEnv("POSTGRES_USER");
string postgresPasswd = GetEnv("POSTGRES_PASSWORD");
string postgresDb = GetEnv("POSTGRES_DATABASE");

// Kafka config
string kafkaGroupId = GetEnv("KAFKA_GROUP_ID");
string kafkaHost = GetEnv("KAFKA_HOST");
string kafkaPort = GetEnv("KAFKA_PORT");
string kafkaAutoOffsetReset = GetEnv("KAFKA_AUTO_OFFSET_RESET");
bool kafkaEnableAutoCommit = GetEnv("KAFKA_ENABLE_AUTO_COMMIT").ToLower() == "true" ? true : false;
bool kafkaAllowAutoCreateTopics =
    GetEnv("KAFKA_ALLOW_AUTO_CREATE_TOPICS").ToLower() == "true" ? true : false;

AutoOffsetReset autoOffsetReset;
switch (kafkaAutoOffsetReset.ToLower())
{
    case "earliest":
        autoOffsetReset = AutoOffsetReset.Earliest;
        break;

    case "latest":
        autoOffsetReset = AutoOffsetReset.Latest;
        break;

    case "error":
        autoOffsetReset = AutoOffsetReset.Error;
        break;

    default:
        throw new Exception(
            $"'{kafkaAutoOffsetReset}' is not an acceptable value for the KAFKA_AUTO_OFFSET_RESET variable."
        );
}

Action<DbContextOptionsBuilder> configureDbContext = (
    o =>
        o.UseLazyLoadingProxies()
            .UseNpgsql(
                $"Host={postgresHost}; Database={postgresDb}; Username={postgresUser}; Password={postgresPasswd}"
            )
);

Action<ConsumerConfig> configureConsumer = (
    o =>
    {
        o.BootstrapServers = $"{kafkaHost}:{kafkaPort}";
        o.GroupId = kafkaGroupId;
        o.EnableAutoCommit = kafkaEnableAutoCommit;
        o.AllowAutoCreateTopics = kafkaAllowAutoCreateTopics;
        o.AutoOffsetReset = autoOffsetReset;
    }
);

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices(services =>
{
    services.AddDbContext<DatabaseContext>(configureDbContext);
    services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

    var dataContext = services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
    dataContext.Database.EnsureCreated();

    services.AddScoped<IReservationRepository, ReservationRepository>();
    services.AddScoped<ICityRepository, CityRepository>();
    services.AddScoped<ICountryRepository, CountryRepository>();
    services.AddScoped<IHotelRepository, HotelRepository>();
    services.AddScoped<IRoomRepository, RoomRepository>();
    services.AddScoped<IRoomReservedRepository, RoomReservedRepository>();
    services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

    services.AddScoped<IQueryHandler, QueryHandler>();
    services.AddScoped<IEventHandler, Hotel.Query.Infrastructure.Handlers.EventHandler>();
    services.Configure<ConsumerConfig>(configureConsumer);
    services.AddScoped<IEventConsumer, EventConsumer>();
    services.AddScoped<IQueryConsumer, QueryConsumer>();

    services.AddHostedService<EventConsumerHostedService>();
    services.AddHostedService<QueryConsumerHostedService>();
});

using var host = builder.Build();

host.Run();
