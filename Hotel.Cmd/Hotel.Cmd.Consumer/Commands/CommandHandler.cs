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

    public async Task HandleAsync(ReserveHotelCommand command)
    {
        var aggregate = new ReservationAggregate(
            command.Id,
            command.User,
            command.StartDate,
            command.EndDate,
            command.TotalPrice,
            command.Hotel,
            command.RoomReserved
        );

        await _eventSourcingHandler.SaveAsync(aggregate);
    }

    public async Task HandleAsync(EditReservationCommand command)
    {
        var aggregate = await _eventSourcingHandler.GetByIdAsync(command.Id);
        aggregate.EditReservation(
            command.StartDate,
            command.EndDate,
            command.TotalPrice,
            command.RoomReserved
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
