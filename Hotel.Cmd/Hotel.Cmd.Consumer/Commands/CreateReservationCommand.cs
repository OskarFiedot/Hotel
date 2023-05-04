using CQRS.Core.Commands;

namespace Hotel.Cmd.Commands;

class CreateReservationCommand : BaseCommand
{
    public string User { get; set; }
    public Guid TripId { get; set; }
    public int NumberOfAdults { get; set; }
    public int NumberOfChildrenUpTo3yo { get; set; }
    public int NumberOfChildrenUpTo10yo { get; set; }
    public int NumberOfChildrenUpTo18yo { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public string PlaceOfDeparture { get; set; }
    public float TotalPrice { get; set; }
}
