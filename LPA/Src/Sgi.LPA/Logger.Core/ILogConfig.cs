using System.Collections.Generic;

namespace Logger.Core
{
    public interface ILogConfig
    {
        List<int> LogLevels { get; set; }
    }
}