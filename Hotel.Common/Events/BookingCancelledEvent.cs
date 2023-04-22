namespace Hotel.Common.Events;
using CQRS.Core.Events;

class BookingCancelledEvent : BaseEvent
{
    public BookingCancelledEvent() : base(nameof(BookingCancelledEvent))
    {
    }
}