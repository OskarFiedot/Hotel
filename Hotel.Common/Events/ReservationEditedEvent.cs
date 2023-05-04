using CQRS.Core.Events;

namespace Hotel.Common.Events;

public class ReservationEditedEvent : BaseEvent
{
    public ReservationEditedEvent()
        : base(nameof(ReservationEditedEvent)) { }

    public int NumberOfAdults { get; set; }
    public int NumberOfChildrenUpTo3yo { get; set; }
    public int NumberOfChildrenUpTo10yo { get; set; }
    public int NumberOfChildrenUpTo18yo { get; set; }
    public DateTime StartDate { get; set; }
    public int Duration { get; set; }
    public string PlaceOfDeparture { get; set; }
    public float TotalPrice { get; set; }
    public DateTime DateUpdated { get; set; }
}
