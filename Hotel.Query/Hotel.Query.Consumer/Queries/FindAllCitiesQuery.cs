using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllCitiesQuery : BaseQuery
{
    public FindAllCitiesQuery()
        : base(nameof(FindAllCitiesQuery)) { }
}
