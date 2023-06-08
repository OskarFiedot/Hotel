using CQRS.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hotel.Query.Consumer.Consumers;

public class QueryConsumerHostedService : IHostedService
{
    private readonly ILogger<QueryConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public QueryConsumerHostedService(
        ILogger<QueryConsumerHostedService> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Query consumer service running.");

        using IServiceScope scope = _serviceProvider.CreateScope();

        var queryConsumer = scope.ServiceProvider.GetRequiredService<IQueryConsumer>();
        var queue = Environment.GetEnvironmentVariable("RABBIT_QUEUE");

        Task.Run(() => queryConsumer.Consume(queue), cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Query consumer service stopped.");

        return Task.CompletedTask;
    }
}
