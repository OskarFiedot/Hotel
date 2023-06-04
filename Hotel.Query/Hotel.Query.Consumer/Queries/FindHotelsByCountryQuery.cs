using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindHotelsByCountryQuery : BaseQuery
{
    public FindHotelsByCountryQuery()
        : base(nameof(FindHotelsByCountryQuery)) { }

    public string CountryName { get; set; }
}
