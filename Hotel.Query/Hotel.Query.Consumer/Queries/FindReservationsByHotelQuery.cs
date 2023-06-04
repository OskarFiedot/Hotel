using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByHotelQuery : BaseQuery
{
    public FindReservationsByHotelQuery()
        : base(nameof(FindReservationsByHotelQuery)) { }

    public Guid HotelId { get; set; }
}
