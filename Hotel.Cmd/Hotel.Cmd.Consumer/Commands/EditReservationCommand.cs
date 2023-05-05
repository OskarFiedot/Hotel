using CQRS.Core.Commands;

namespace Hotel.Cmd.Commands;

class EditReservationCommand : BaseCommand
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public List<Guid> RoomReserved { get; set; }
}
