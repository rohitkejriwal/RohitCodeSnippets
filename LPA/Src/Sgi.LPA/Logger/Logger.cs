using Logger.Core;
using Logger.Domain;

namespace Logger
{
    public class Logger : ILogger
    {
        private ILogConfig _config;
        private ILogWriter _writer;

        public Logger(ILogConfig config, ILogWriter writer)
        {
            _config = config;
            _writer = writer;
        }

        public void Debug(DebugLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Debug))
            {
                WriteLog(logData);
            }
        }

        public void Info(InfoLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Info))
            {
                WriteLog(logData);
            }
        }

        public void Error(ErrorLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Error))
            {
                WriteLog(logData);
            }
        }

        public void Enter(EnterLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Enter))
            {
                WriteLog(logData);
            }
        }

        public void Exit(ExitLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Exit))
            {
                WriteLog(logData);
            }
        }

        private void WriteLog(LogBaseData data)
        {
            _writer.WriteLog(data);
        }

        public void Timespan(ExecutionStepLoggerData logData)
        {
            if (_config.LogLevels.Contains(LogLevels.Timespan))
            {
                WriteLog(logData);
            }
        }
    }
}