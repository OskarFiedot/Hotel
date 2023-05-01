using CQRS.Core.Commands;

namespace Hotel.Cmd.Commands;

class CancelReservationCommand : BaseCommand
{
    public string User { get; set; }
}
