using Hotel.Cmd.Infrastructure.Config;
using Hotel.Cmd.Infrastructure.Repositories;
using Hotel.Cmd.Infrastructure.Stores;

string mongo_user = GetEnv("MONGO_USER");
string mongo_passwd = GetEnv("MONGO_PASSWORD");
string mongo_host = GetEnv("MONGO_HOST");
string mongo_port = GetEnv("MONGO_PORT");
string mongo_db = GetEnv("MONGO_DATABASE");
string mongo_coll = GetEnv("MONGO_COLLECTION");

MongoDbConfig mongo_config =
    new(mongo_user, mongo_passwd, mongo_host, mongo_port, mongo_db, mongo_coll);
EventStoreRepository mongo_repo = new(mongo_config);
EventStore mongo_es = new(mongo_repo);

string GetEnv(string envName)
{
    string envValue = Environment.GetEnvironmentVariable(envName);

    if (string.IsNullOrEmpty(envValue))
    {
        throw new Exception($"{envName} environment variable is not set.");
    }

    return envValue;
}
