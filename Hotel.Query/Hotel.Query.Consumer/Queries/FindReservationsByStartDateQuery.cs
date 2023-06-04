using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByStartDateQuery : BaseQuery
{
    public FindReservationsByStartDateQuery()
        : base(nameof(FindReservationsByStartDateQuery)) { }

    public DateTime StartDate { get; set; }
}
