using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomsByHotelQuery : BaseQuery
{
    public FindRoomsByHotelQuery()
        : base(nameof(FindRoomsByHotelQuery)) { }

    public Guid HotelId { get; set; }
}
