using Logger.Domain;

namespace Logger.Core
{
    public interface ILogger
    {
        void Debug(DebugLoggerData logData);

        void Info(InfoLoggerData logData);

        void Error(ErrorLoggerData logData);

        void Enter(EnterLoggerData logData);

        void Exit(ExitLoggerData logData);

        void Timespan(ExecutionStepLoggerData logData);
    }
}