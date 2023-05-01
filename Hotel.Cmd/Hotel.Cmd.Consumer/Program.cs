﻿using Hotel.Cmd.Commands;
using Hotel.Cmd.Infrastructure.Config;
using Hotel.Cmd.Infrastructure.Dispatchers;
using Hotel.Cmd.Infrastructure.Handlers;
using Hotel.Cmd.Infrastructure.Repositories;
using Hotel.Cmd.Infrastructure.Stores;

string mongoUser = GetEnv("MONGO_USER");
string mongoPasswd = GetEnv("MONGO_PASSWORD");
string mongoHost = GetEnv("MONGO_HOST");
string mongoPort = GetEnv("MONGO_PORT");
string mongoDb = GetEnv("MONGO_DATABASE");
string mongoColl = GetEnv("MONGO_COLLECTION");

MongoDbConfig mongoConfig = new(mongoUser, mongoPasswd, mongoHost, mongoPort, mongoDb, mongoColl);
EventStoreRepository eventStoreRepo = new(mongoConfig);
EventStore eventStore = new(eventStoreRepo);
EventSourcingHandler esHandler = new(eventStore);
CommandHandler commandHandler = new(esHandler);

// Register command handler methods
CommandDispatcher dispatcher = new();
dispatcher.RegisterHandler<ReserveHotelCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditReservationCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<CancelReservationCommand>(commandHandler.HandleAsync);

string GetEnv(string envName)
{
    string envValue = Environment.GetEnvironmentVariable(envName);

    if (string.IsNullOrEmpty(envValue))
    {
        throw new Exception($"{envName} environment variable is not set.");
    }

    return envValue;
}
