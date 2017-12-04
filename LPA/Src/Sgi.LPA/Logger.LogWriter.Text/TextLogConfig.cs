using System.Collections.Generic;
using System.Configuration;

namespace Logger.LogWriter.Text
{
    public class TextLogConfig : ITextLogConfig
    {
        public string FilePath { get; set; }
        public List<int> LogLevels { get; set; }

        public TextLogConfig()
        {
            FilePath = ConfigurationManager.AppSettings["LogFilePath"] != null ? ConfigurationManager.AppSettings["LogFilePath"] : @"C:\temp\IOLog.txt";
            AddInitLogLevels();
        }

        private void AddInitLogLevels()
        {
            LogLevels = new List<int>();
            if (ConfigurationManager.AppSettings["LogLevel"] != null)
            {
                foreach (var item in ConfigurationManager.AppSettings["LogLevel"].Split(','))
                {
                    int outdata = 0;
                    if (int.TryParse(item, out outdata))
                    {
                        LogLevels.Add(outdata);
                    }
                }
            }
            else
            {
                LogLevels = Logger.Domain.LogLevels.DefaultLevels;
            }
        }
    }
}