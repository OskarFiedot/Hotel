using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByRoomQuery : BaseQuery
{
    public FindReservationsByRoomQuery()
        : base(nameof(FindReservationsByRoomQuery)) { }

    public Guid RoomId { get; set; }
}
