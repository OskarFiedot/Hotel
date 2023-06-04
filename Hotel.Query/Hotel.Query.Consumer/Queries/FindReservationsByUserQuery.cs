using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByUserQuery : BaseQuery
{
    public FindReservationsByUserQuery()
        : base(nameof(FindReservationsByUserQuery)) { }

    public string User { get; set; }
}
