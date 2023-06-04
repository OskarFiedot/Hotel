using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindHotelByIdQuery : BaseQuery
{
    public FindHotelByIdQuery()
        : base(nameof(FindHotelByIdQuery)) { }

    public Guid Id { get; set; }
}
