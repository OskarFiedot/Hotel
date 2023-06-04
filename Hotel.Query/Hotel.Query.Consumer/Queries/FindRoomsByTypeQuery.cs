using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomsByTypeQuery : BaseQuery
{
    public FindRoomsByTypeQuery()
        : base(nameof(FindRoomsByTypeQuery)) { }

    public Guid RoomTypeId { get; set; }
}
