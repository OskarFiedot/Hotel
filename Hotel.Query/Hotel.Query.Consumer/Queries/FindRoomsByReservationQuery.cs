using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomsByReservationQuery : BaseQuery
{
    public FindRoomsByReservationQuery()
        : base(nameof(FindRoomsByReservationQuery)) { }

    public Guid ReservationId { get; set; }
}
