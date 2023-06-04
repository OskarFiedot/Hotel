using CQRS.Core.Entities;
using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;

namespace Hotel.Query.Infrastructure.Dispatchers;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly Dictionary<Type, Func<BaseQuery, Task<List<BaseEntity>>>> _handlers = new();

    public void RegisterHandler<TQuery>(Func<TQuery, Task<List<BaseEntity>>> handler)
        where TQuery : BaseQuery
    {
        if (_handlers.ContainsKey(typeof(TQuery)))
        {
            throw new IndexOutOfRangeException("You cannot register the same query twice.");
        }

        _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
    }

    public async Task<List<BaseEntity>> SendAsync(BaseQuery query)
    {
        if (
            _handlers.TryGetValue(
                query.GetType(),
                out Func<BaseQuery, Task<List<BaseEntity>>> handler
            )
        )
        {
            return await handler(query);
        }

        throw new ArgumentNullException(nameof(handler), "No query handler was registered.");
    }
}
