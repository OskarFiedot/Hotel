using CQRS.Core.Commands;

namespace Hotel.Cmd.Consumer.Commands;

class CancelReservationCommand : BaseCommand
{
    public CancelReservationCommand()
        : base(nameof(CancelReservationCommand)) { }

    public string User { get; set; }
}
