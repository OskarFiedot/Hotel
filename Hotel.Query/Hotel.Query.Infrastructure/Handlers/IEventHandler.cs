using Hotel.Common.Events;

namespace Hotel.Query.Infrastructure.Handlers;

public interface IEventHandler
{
    Task On(ReservationCreatedEvent @event);
    Task On(ReservationEditedEvent @event);
    Task On(ReservationCancelledEvent @event);
}
