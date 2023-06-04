using CQRS.Core.Queries;

namespace Hotel.Query.Consumer.Queries;

public class FindCityByIdQuery : BaseQuery
{
    public FindCityByIdQuery()
        : base(nameof(FindCityByIdQuery)) { }

    public Guid Id { get; set; }
}
