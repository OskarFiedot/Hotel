using CQRS.Core.Commands;

namespace Hotel.Cmd.Consumer.Commands;

class EditReservationCommand : BaseCommand
{
    public EditReservationCommand()
        : base(nameof(EditReservationCommand)) { }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public List<Guid> RoomReserved { get; set; }
}
