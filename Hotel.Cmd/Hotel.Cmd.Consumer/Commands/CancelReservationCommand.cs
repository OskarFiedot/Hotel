namespace Hotel.Cmd.Commands;
using CQRS.Core.Commands;

class CancelReservationCommand : BaseCommand
{
    public string User { get; set; }
}