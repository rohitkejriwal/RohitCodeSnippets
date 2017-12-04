using System.Collections.Generic;

namespace Logger.Domain
{
    public class InfoLoggerData : DebugLoggerData
    {
        public Dictionary<string, object> ChangedParams { get; set; }

        public InfoLoggerData()
        {
            Level = LogLevels.Info;
        }
    }
}