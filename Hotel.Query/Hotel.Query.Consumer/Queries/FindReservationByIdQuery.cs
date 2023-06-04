using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindReservationByIdQuery : BaseQuery
{
    public FindReservationByIdQuery()
        : base(nameof(FindReservationByIdQuery)) { }

    public Guid Id { get; set; }
}
