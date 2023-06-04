using CQRS.Core.Entities;
using CQRS.Core.Queries;

namespace CQRS.Core.Infrastructure;

public interface IQueryDispatcher
{
    void RegisterHandler<TQuery>(Func<TQuery, Task<List<BaseEntity>>> handler)
        where TQuery : BaseQuery;
    Task<List<BaseEntity>> SendAsync(BaseQuery query);
}
