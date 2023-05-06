using CQRS.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hotel.Query.Infrastructure.Consumers;

public class CommandConsumerHostedService : IHostedService
{
    private readonly ILogger<CommandConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CommandConsumerHostedService(
        ILogger<CommandConsumerHostedService> logger,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event consumer service running.");

        using IServiceScope scope = _serviceProvider.CreateScope();

        var commandConsumer = scope.ServiceProvider.GetRequiredService<ICommandConsumer>();
        var queue = Environment.GetEnvironmentVariable("RABBIT_QUEUE");

        Task.Run(() => commandConsumer.Consume(queue), cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event consumer service stopped.");

        return Task.CompletedTask;
    }
}
