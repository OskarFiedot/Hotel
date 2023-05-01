using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Hotel.Cmd.Domain.Aggregates;

namespace Hotel.Cmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<ReservationAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<ReservationAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new ReservationAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any())
        {
            return aggregate;
        }

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;
    }

    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.Changes, aggregate.Version);
        aggregate.MarkChangesAsCommited();
    }
}
