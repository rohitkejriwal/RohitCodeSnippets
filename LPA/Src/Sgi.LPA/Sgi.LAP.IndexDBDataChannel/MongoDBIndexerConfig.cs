using Sgi.MongoDBService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LAP.IndexDBDataChannel
{
    public class MongoDBIndexerConfig : IMongoDBConfig
    {
        public string ServerIP { get; set; }
        public string Database { get; set; }
        public string DefaultCollection { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DBPort { get; set; }

        public MongoDBIndexerConfig()
        {
            ServerIP = ConfigurationManager.AppSettings["IndexerMongoServerIP"];
            Database = ConfigurationManager.AppSettings["IndexerMongoDatabase"];
            DefaultCollection = ConfigurationManager.AppSettings["IndexerDefaultCollection"];
            UserName = ConfigurationManager.AppSettings["IndexerMongoUser"];
            Password = ConfigurationManager.AppSettings["IndexerMongoPassword"];
            DBPort = int.Parse(ConfigurationManager.AppSettings["IndexerMongoPort"]);
        }
    }
}
