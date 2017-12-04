using Logger.Core;

namespace Logger.LogWriter.Mongo
{
    public interface IMongoLogConfig : ILogConfig
    {
        string MongoLogServerIP { get; }
        int MongoLogServerPort { get; }
        string MongoLogServerUser { get; }
        string MongoLogServerPassword { get; }
        string MongoLogDatabase { get; }
        string LogCollection { get; }
    }
}