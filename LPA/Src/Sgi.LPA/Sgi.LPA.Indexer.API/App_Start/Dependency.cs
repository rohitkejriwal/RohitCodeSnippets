using IOCInfrastructure;
using Logger.Core;
using Logger.LogWriter.Mongo;
using Sgi.Core.DBService;
using Sgi.LPA.Indexer.Service;
using Sgi.MongoDBService;
using Sgi.MongoDBService.Interfaces;

namespace Sgi.LPA.Indexer.API.App_Start
{
    public class Dependency
    {
        public void AddDepenency(IServiceRegister container)
        {
            var config = new MongoLogConfig();
            container.RegisterInstance<IMongoLogConfig>(config, ServiceLifetime.Single);
            container.Register<ILogWriter, MongoLogWriter>(ServiceLifetime.Single);
            container.RegisterInstance<ILogConfig>(config, ServiceLifetime.Single);
            container.Register<ILogHelper, LPAIndexerLogHelper>();

            container.RegisterInstance<IMongoDBConfig>(new MongoDBConfig(), ServiceLifetime.Single);
            container.Register<IDBRepository, MongoRepository>();
            container.Register<IIndexerService, IndexerService>();
        }
    }
}