using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllRoomsReservationsQuery : BaseQuery
{
    public FindAllRoomsReservationsQuery()
        : base(nameof(FindAllRoomsReservationsQuery)) { }
}
