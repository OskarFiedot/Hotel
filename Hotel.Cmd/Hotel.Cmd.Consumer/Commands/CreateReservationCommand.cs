using CQRS.Core.Commands;

namespace Hotel.Cmd.Commands;

class CreateReservationCommand : BaseCommand
{
    public string User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public Guid Hotel { get; set; }
    public List<Guid> RoomReserved { get; set; }
}
