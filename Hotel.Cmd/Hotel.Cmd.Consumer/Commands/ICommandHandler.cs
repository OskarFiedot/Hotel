namespace Hotel.Cmd.Commands;

interface ICommandHandler
{
    Task HandleAsync(CreateReservationCommand command);
    Task HandleAsync(EditReservationCommand command);
    Task HandleAsync(CancelReservationCommand command);
}
