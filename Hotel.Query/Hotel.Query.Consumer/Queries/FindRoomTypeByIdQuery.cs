using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomTypeByIdQuery : BaseQuery
{
    public FindRoomTypeByIdQuery()
        : base(nameof(FindRoomTypeByIdQuery)) { }

    public Guid Id { get; set; }
}
