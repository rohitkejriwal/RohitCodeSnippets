using Sgi.MongoDBService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.MongoDBService
{
    public class MongoDBConfig : IMongoDBConfig
    {
        public string ServerIP { get; set; }
        public string Database { get; set; }
        public string DefaultCollection { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DBPort { get; set; }
        public MongoDBConfig()
        {
            ServerIP = ConfigurationManager.AppSettings["MongoServerIP"];
            Database = ConfigurationManager.AppSettings["MongoDatabase"];
            DefaultCollection = ConfigurationManager.AppSettings["DefaultCollection"];
            UserName = ConfigurationManager.AppSettings["MongoUser"];
            Password = ConfigurationManager.AppSettings["MongoPassword"];
            DBPort = int.Parse(ConfigurationManager.AppSettings["MongoPort"]);
        }

    }
}
