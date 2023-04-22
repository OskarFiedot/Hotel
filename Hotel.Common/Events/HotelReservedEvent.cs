namespace Hotel.Common.Events;
using CQRS.Core.Events;

class HotelReservedEvent : BaseEvent
{
    public HotelReservedEvent() : base(nameof(HotelReservedEvent))
    {
    }
}