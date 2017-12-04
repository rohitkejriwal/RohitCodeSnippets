using IOCInfrastructure;
using Logger.Core;
using Logger.LogWriter.Mongo;
using Microsoft.Practices.Unity;
using Sgi.Core.DBService;
using Sgi.LAP.AzureSearchDataChannel;
using Sgi.LAP.IndexDBDataChannel;
using Sgi.LPA.AI;
using Sgi.LPA.ChatEngine;
using Sgi.LPA.ChatStorage;
using Sgi.LPA.Common.AI;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.NLP;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.NLPParser;
using Sgi.LPA.UAM;
using Sgi.MongoDBService;
using Sgi.MongoDBService.Interfaces;

namespace Sgi.LAP.API.App_Start
{
    public class Dependency
    {
        public void AddDepenency(IServiceRegister container)
        {
            //var config = new TextLogConfig();
            
            //Dictionary<string, ILogWriter> logWriter = new Dictionary<string, ILogWriter>();
            //logWriter.Add("textwriter", new TextLogWriter(config));

            //container.RegisterInstance<ILogWriter>(logWriter, ServiceLifetime.Single,null);
            //container.RegisterInstance<ILogWriter>(new TextLogWriter(config), ServiceLifetime.Single);

            var config = new MongoLogConfig();
            container.RegisterInstance<IMongoLogConfig>(config, ServiceLifetime.Single);
            container.Register<ILogWriter, MongoLogWriter>(ServiceLifetime.Single);
            container.RegisterInstance<ILogConfig>(config, ServiceLifetime.Single);
            container.Register<ILogHelper, LAPLogHelper>();

            container.Register<INLPParser, Luis>();
            container.Register<IChatEngine, LPAChat>();
            container.Register<ILPAUAM, UAP>();
            container.Register<IUserContext, UserContext>();
            container.Register<IAIEngine, AIEngine>();
            //container.Register<IDataChannel, AzureSearch>("azuresearch");
            container.Register<IDataChannel, IndexDBSearch>(LPAConsts.DataChannelIndexDBSearchKey);
            container.Register<IDataChannel, CommonDataChannel>(LPAConsts.DataChannelCommonKey );


            container.Register<IResponseFormatter, ResponseFormatter>();


            container.RegisterInstance<IMongoDBConfig>(new MongoDBConfig(), LPAConsts.MongoDBDefaultConfigKey);
            container.RegisterInstance<IMongoDBConfig>(new MongoDBIndexerConfig(), LPAConsts.MongoDBIndexDBConfigKey);
            container.RegisterInstance<IMongoDBConfig>(new MongoDBUserConfig(), LPAConsts.MongoDBUserDBConfigKey);

            container.Register<IDBRepository, MongoRepository>(LPAConsts.MongoDBChatRepositoryKey, new InjectionConstructor(
                new ResolvedParameter<IMongoDBConfig>(LPAConsts.MongoDBDefaultConfigKey)));

            container.Register<IDBRepository, MongoRepository>(LPAConsts.MongoDBIndexRepositoryKey, new InjectionConstructor(
                new ResolvedParameter<IMongoDBConfig>(LPAConsts.MongoDBIndexDBConfigKey)));

            container.Register<IDBRepository, MongoRepository>(LPAConsts.MongoDBUserRepositoryKey, new InjectionConstructor(
               new ResolvedParameter<IMongoDBConfig>(LPAConsts.MongoDBUserDBConfigKey)));

            container.Register<IDataCrawlerSource, DBDataService>(LPAConsts.DataCrawlerDbSourceKey);
            container.Register<IDataCrawlerSource, WebCrawlerDataService>(LPAConsts.WebCrawlerDataSourceKey);

            container.Register<IChatContext, ChatContext>();
        }
    }
}