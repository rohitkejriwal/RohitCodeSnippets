using System;
using System.Collections.Generic;
using System.Configuration;

namespace Logger.LogWriter.Mongo
{
    public class MongoLogConfig : IMongoLogConfig
    {
        public string MongoLogServerIP { get; private set; }
        public int MongoLogServerPort { get; private set; }
        public string MongoLogServerUser { get; private set; }
        public string MongoLogServerPassword { get; private set; }
        public string MongoLogDatabase { get; private set; }
        public string LogCollection { get; private set; }
        public List<int> LogLevels { get; set; }

        public MongoLogConfig()
        {
            MongoLogServerIP = Convert.ToString(ConfigurationManager.AppSettings["MongoLogServerIP"]);
            MongoLogServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["MongoLogServerPort"]);
            MongoLogServerUser = Convert.ToString(ConfigurationManager.AppSettings["MongoLogServerUser"]);
            MongoLogServerPassword = Convert.ToString(ConfigurationManager.AppSettings["MongoLogServerPassword"]);
            MongoLogDatabase = Convert.ToString(ConfigurationManager.AppSettings["MongoLogDatabase"]);
            LogCollection = Convert.ToString(ConfigurationManager.AppSettings["MongoLogCollection"]);
            AddInitLogLevels();
        }

        private void AddInitLogLevels()
        {
            LogLevels = new List<int>();
            foreach (var item in ConfigurationManager.AppSettings["LogLevel"].Split(','))
            {
                int outdata = 0;
                if (int.TryParse(item, out outdata))
                {
                    LogLevels.Add(outdata);
                }
            }
        }
    }
}