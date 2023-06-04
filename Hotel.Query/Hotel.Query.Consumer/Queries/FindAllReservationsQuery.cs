using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindAllReservationsQuery : BaseQuery
{
    public FindAllReservationsQuery()
        : base(nameof(FindAllReservationsQuery)) { }
}
