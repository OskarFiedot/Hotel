namespace Hotel.Cmd.Consumer.Commands;

interface ICommandHandler
{
    Task HandleAsync(CreateReservationCommand command);
    Task HandleAsync(EditReservationCommand command);
    Task HandleAsync(CancelReservationCommand command);
}
