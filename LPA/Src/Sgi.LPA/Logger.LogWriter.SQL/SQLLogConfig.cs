using System;
using System.Collections.Generic;
using System.Configuration;

namespace Logger.LogWriter.SQL
{
    public class SQLLogConfig : ISQLLogConfig
    {
        public string ConnectionString { get; set; }
        public List<int> LogLevels { get; set; }

        public SQLLogConfig()
        {
            ConnectionString = Convert.ToString(ConfigurationManager.AppSettings["LogConnectionString"]);
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