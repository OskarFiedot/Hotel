using CQRS.Core.Handlers;
using Hotel.Cmd.Domain.Aggregates;

namespace Hotel.Cmd.Commands;

class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<ReservationAggregate> _eventSourcingHandler;

    public CommandHandler(IEventSourcingHandler<ReservationAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandleAsync(CreateReservationCommand command)
    {
        var aggregate = new ReservationAggregate(
            command.Id,
            command.User,
            command.TripId,
            command.NumberOfAdults,
            command.NumberOfChildrenUpTo3yo,
            command.NumberOfChildrenUpTo10yo,
            command.NumberOfChildrenUpTo18yo,
            command.StartDate,
            command.Duration,
            command.PlaceOfDeparture,
            command.TotalPrice
        );

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditReservationCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditReservation(
            command.NumberOfAdults,
            command.NumberOfChildrenUpTo3yo,
            command.NumberOfChildrenUpTo10yo,
            command.NumberOfChildrenUpTo18yo,
            command.StartDate,
            command.Duration,
            command.PlaceOfDeparture,
            command.TotalPrice
        );

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(CancelReservationCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.CancelReservation(command.User);

        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}
