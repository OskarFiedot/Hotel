using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindHotelsByCityQuery : BaseQuery
{
    public FindHotelsByCityQuery()
        : base(nameof(FindHotelsByCityQuery)) { }

    public string CityName { get; set; }
}
