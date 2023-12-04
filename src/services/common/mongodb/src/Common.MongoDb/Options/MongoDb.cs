namespace Common.MongoDb.Options;

public class MongoDb
{
    public string Host { get; init; } = string.Empty;
    public int    Port { get; init; }

    public string ConnectionString => $"mongodb://{Host}:{Port}";
}
