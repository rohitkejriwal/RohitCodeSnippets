using Sgi.MongoDBService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.UAM
{
    public class MongoDBUserConfig: IMongoDBConfig
    {
        public string ServerIP { get; set; }
        public string Database { get; set; }
        public string DefaultCollection { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DBPort { get; set; }

        public MongoDBUserConfig()
        {
            ServerIP = ConfigurationManager.AppSettings["UserMongoServerIP"];
            Database = ConfigurationManager.AppSettings["UserMongoDatabase"];
            DefaultCollection = ConfigurationManager.AppSettings["UserDefaultCollection"];
            UserName = ConfigurationManager.AppSettings["UserMongoUser"];
            Password = ConfigurationManager.AppSettings["UserMongoPassword"];
            DBPort = int.Parse(ConfigurationManager.AppSettings["UserMongoPort"]);
        }
    }
}
