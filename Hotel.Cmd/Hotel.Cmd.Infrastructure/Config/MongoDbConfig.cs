namespace Hotel.Cmd.Infrastructure.Config;

public class MongoDbConfig
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string Collection { get; set; }

    public MongoDbConfig(
        string user,
        string passwd,
        string host,
        string port,
        string database,
        string collection
    )
    {
        ConnectionString = $"mongodb://{user}:{passwd}@{host}:{port}";
        Database = database;
        Collection = collection;
    }

    public MongoDbConfig(string connectionString, string database, string collection)
    {
        ConnectionString = connectionString;
        Database = database;
        Collection = collection;
    }
}
