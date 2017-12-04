using System.Collections.Generic;

namespace Logger.Domain
{
    public class DebugLoggerData : LogBaseData
    {
        public Dictionary<string, object> Params { get; set; }
        public string MethodName { get; set; }

        public DebugLoggerData()
        {
            Level = LogLevels.Debug;
        }
    }
}