using Logger.Core;
using Logger.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class MockLogger : ILogger
    {
        public void Debug(Logger.Domain.DebugLoggerData logData)
        {
            Write(logData);
        }

        public void Info(Logger.Domain.InfoLoggerData logData)
        {
            Write(logData);
        }

        public void Error(Logger.Domain.ErrorLoggerData logData)
        {
            Write(logData);
        }

        public void Enter(Logger.Domain.EnterLoggerData logData)
        {
            Write(logData);
        }

        public void Exit(Logger.Domain.ExitLoggerData logData)
        {
            Write(logData);
        }

        public void Timespan(Logger.Domain.ExecutionStepLoggerData logData)
        {
            Write(logData);
        }
        private void Write(LogBaseData logdata)
        {
            Console.Write(logdata.Message);
        }
    }
    public class MockLogWriter : ILogWriter
    {
        public void WriteLog(Logger.Domain.LogBaseData log)
        {
            Console.Write(log.Message);
        }
    }
}
