using System.Collections.Generic;

namespace Logger.Domain
{
    public class ExitLoggerData : LogBaseData
    {
        public Dictionary<string, object> OutputParams { get; set; }
        public string MethodName { get; set; }

        public ExitLoggerData()
        {
            OutputParams = new Dictionary<string, object>();
            Level = LogLevels.Exit;
        }
    }
}