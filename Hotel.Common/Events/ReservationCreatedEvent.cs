using CQRS.Core.Events;

namespace Hotel.Common.Events;

public class ReservationCreatedEvent : BaseEvent
{
    public ReservationCreatedEvent()
        : base(nameof(ReservationCreatedEvent)) { }

    public string User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public Guid Hotel { get; set; }
    public List<Guid> RoomReserved { get; set; }
    public DateTime DateReserved { get; set; }
}
