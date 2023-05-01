namespace Hotel.Cmd.Commands;

interface ICommandHandler
{
    Task HandleAsync(ReserveHotelCommand command);
    Task HandleAsync(EditReservationCommand command);
    Task HandleAsync(CancelReservationCommand command);
}
