using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindCountryByIdQuery : BaseQuery
{
    public FindCountryByIdQuery()
        : base(nameof(FindCountryByIdQuery)) { }

    public Guid Id { get; set; }
}
