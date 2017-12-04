using Sgi.MongoDBService.Interfaces;
using System.Configuration;

namespace SGI.LPA.Crawler.Service
{
    public class MongoDbWebCrawlerConfig : IMongoDBConfig
    {
        public string ServerIP { get; set; }
            public string Database { get; set; }
            public string DefaultCollection { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public int DBPort { get; set; }

            public MongoDbWebCrawlerConfig()
            {
                ServerIP = ConfigurationManager.AppSettings["WebCrawlerMongoServerIP"];
                Database = ConfigurationManager.AppSettings["WebCrawlerMongoDatabase"];
                DefaultCollection = ConfigurationManager.AppSettings["WebCrawlerDefaultCollection"];
                UserName = ConfigurationManager.AppSettings["WebCrawlerMongoUser"];
                Password = ConfigurationManager.AppSettings["WebCrawlerMongoPassword"];
                DBPort = int.Parse(ConfigurationManager.AppSettings["WebCrawlerMongoPort"]);
            }
        
    }
}
