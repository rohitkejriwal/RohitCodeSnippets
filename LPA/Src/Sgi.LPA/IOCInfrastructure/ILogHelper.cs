using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IOCInfrastructure
{
    public interface ILogHelper
    {
        void LogDebug(string methodName, string message, System.Collections.Generic.Dictionary<string, object> debugParams, object parent = null);

        void LogEnter(string methodName, Dictionary<string, object> inputParams, string message, object parent = null);

        void LogError(Exception ex, string className, string methodName, System.Collections.Generic.Dictionary<string, object> inputParams, object parent = null);

        void LogExit(string methodName, System.Collections.Generic.Dictionary<string, object> outputParams, object parent = null);

        void LogInfo(string methodName, string message, System.Collections.Generic.Dictionary<string, object> debugParams, object parent = null);

        void LogStep(string message, object parent = null, Stopwatch stopwatch = null);
    }
}