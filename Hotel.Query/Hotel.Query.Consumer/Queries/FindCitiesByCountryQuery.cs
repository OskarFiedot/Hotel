using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindCitiesByCountryQuery : BaseQuery
{
    public FindCitiesByCountryQuery()
        : base(nameof(FindCitiesByCountryQuery)) { }

    public string CountryName { get; set; }
}
