using CQRS.Core.Events;

namespace Hotel.Common.Events;

public class ReservationCancelledEvent : BaseEvent
{
    public ReservationCancelledEvent()
        : base(nameof(ReservationCancelledEvent)) { }
}
