using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomByIdQuery : BaseQuery
{
    public FindRoomByIdQuery()
        : base(nameof(FindRoomByIdQuery)) { }

    public Guid Id { get; set; }
}
