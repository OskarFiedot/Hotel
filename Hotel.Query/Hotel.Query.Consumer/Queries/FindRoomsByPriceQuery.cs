using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindRoomsByPriceQuery : BaseQuery
{
    public FindRoomsByPriceQuery()
        : base(nameof(FindRoomsByPriceQuery)) { }

    public float MinPrice { get; set; }
    public float MaxPrice { get; set; }
}
