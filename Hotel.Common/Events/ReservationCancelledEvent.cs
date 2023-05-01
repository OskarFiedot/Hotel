namespace Hotel.Common.Events;
using CQRS.Core.Events;

public class ReservationCancelledEvent : BaseEvent
{
    public ReservationCancelledEvent() : base(nameof(ReservationCancelledEvent))
    {
    }
}