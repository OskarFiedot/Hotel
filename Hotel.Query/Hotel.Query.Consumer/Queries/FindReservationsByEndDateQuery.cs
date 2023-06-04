using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationsByEndDateQuery : BaseQuery
{
    public FindReservationsByEndDateQuery()
        : base(nameof(FindReservationsByEndDateQuery)) { }

    public DateTime EndDate { get; set; }
}
