using CQRS.Core.Events;

namespace Hotel.Common.Events;

public class ReservationEditedEvent : BaseEvent
{
    public ReservationEditedEvent()
        : base(nameof(ReservationEditedEvent)) { }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public List<Guid> RoomReserved { get; set; }
}
