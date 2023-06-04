using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllRoomTypesQuery : BaseQuery
{
    public FindAllRoomTypesQuery()
        : base(nameof(FindAllRoomTypesQuery)) { }
}
