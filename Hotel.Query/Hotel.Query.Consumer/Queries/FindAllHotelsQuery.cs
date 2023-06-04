using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllHotelsQuery : BaseQuery
{
    public FindAllHotelsQuery()
        : base(nameof(FindAllHotelsQuery)) { }
}
