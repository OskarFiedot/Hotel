using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllCountriesQuery : BaseQuery
{
    public FindAllCountriesQuery()
        : base(nameof(FindAllCountriesQuery)) { }
}
