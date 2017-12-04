using Sgi.Core.DBService;

namespace Sgi.MongoDBService.Interfaces
{
    public interface IMongoDBConfig : IDBConfig
    {
        string ServerIP { get; set; }
        string Database { get; set; }
        string DefaultCollection { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        int DBPort { get; set; }
    }
}