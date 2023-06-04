using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByPriceQuery : BaseQuery
{
    public FindReservationsByPriceQuery()
        : base(nameof(FindReservationsByPriceQuery)) { }

    public float MinPrice { get; set; }
    public float MaxPrice { get; set; }
}
