namespace Hotel.Common.Events;
using CQRS.Core.Events;

public class HotelReservedEvent : BaseEvent
{
    public HotelReservedEvent() : base(nameof(HotelReservedEvent))
    {
    }

    public string User { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public float TotalPrice { get; set; }
    public Guid Hotel { get; set; }
    public List<Guid> RoomReserved { get; set; }
    public DateTime DateReserved { get; set; }
}