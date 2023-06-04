using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomReservationByIdQuery : BaseQuery
{
    public FindRoomReservationByIdQuery()
        : base(nameof(FindRoomReservationByIdQuery)) { }

    public Guid Id { get; set; }
}
