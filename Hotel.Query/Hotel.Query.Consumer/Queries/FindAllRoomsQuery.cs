using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllRoomsQuery : BaseQuery
{
    public FindAllRoomsQuery()
        : base(nameof(FindAllRoomsQuery)) { }
}
