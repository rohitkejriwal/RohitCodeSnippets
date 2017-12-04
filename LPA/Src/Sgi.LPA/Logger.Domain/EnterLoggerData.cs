using System.Collections.Generic;

namespace Logger.Domain
{
    public class EnterLoggerData : LogBaseData
    {
        public string MethodName { get; set; }
        public Dictionary<string, object> InputParams { get; set; }

        public EnterLoggerData()
        {
            InputParams = new Dictionary<string, object>();
            Level = LogLevels.Enter;
        }
    }
}