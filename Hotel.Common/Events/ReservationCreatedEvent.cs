using CQRS.Core.Events;

namespace Hotel.Common.Events;

public class ReservationCreatedEvent : BaseEvent
{
    public ReservationCreatedEvent()
        : base(nameof(ReservationCreatedEvent)) { }

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
    public DateTime DateReserved { get; set; }
}
