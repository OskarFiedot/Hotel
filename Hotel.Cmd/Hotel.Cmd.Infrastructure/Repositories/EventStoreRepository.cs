using CQRS.Core.Domain;
using CQRS.Core.Events;
using Hotel.Cmd.Infrastructure.Config;
using MongoDB.Driver;

namespace Hotel.Cmd.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventStoreCollection;

    public EventStoreRepository(MongoDbConfig config)
    {
        var mongoClient = new MongoClient(config.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Database);

        _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Collection);
    }

    public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        return await _eventStoreCollection
            .Find(x => x.AggregateIdentifier == aggregateId)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event)
    {
        await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }
}
