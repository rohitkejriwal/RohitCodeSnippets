using Logger.Core;
using Logger.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Logger.LogWriter.Mongo
{
    public class MongoLogWriter : ILogWriter, IDisposable
    {
        private IMongoLogConfig _config;
        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDataBase;

        public MongoLogWriter(IMongoLogConfig config)
        {
            _config = config;
            OpenConnection();
        }

        public decimal GetDataSize()
        {
            var command = new BsonDocument { { "dataSize", string.Format("{0}.{1}", _config.MongoLogDatabase, _config.LogCollection) } };
            var result = _mongoDataBase.RunCommand<BsonDocument>(command);
            var logCollectionSizeInMb = (result.GetValue("size").ToDouble() / 1024f) / 1024f;
            return Convert.ToDecimal(logCollectionSizeInMb);
        }

        private void OpenConnection()
        {
            var settings = new MongoClientSettings
            {
                Credentials = new[] { MongoCredential.CreateCredential(_config.MongoLogDatabase, _config.MongoLogServerUser, _config.MongoLogServerPassword) },
                Server = new MongoServerAddress(_config.MongoLogServerIP, _config.MongoLogServerPort)
            };
            _mongoClient = new MongoClient(settings);
            _mongoDataBase = _mongoClient.GetDatabase(_config.MongoLogDatabase);
        }

        public void WriteLog(LogBaseData log)
        {
            log._id = string.Format("{0}_{1}_{2}_{3}", Utility.GetRandomNumber(4), Utility.GetRandomString(4), Utility.GetTimeStamp(), log.TransactionId);
            _mongoDataBase.GetCollection<LogBaseData>(_config.LogCollection).InsertOne(log);
        }

        public void Dispose()
        {
        }
    }
}