using Logger.Domain;

namespace Logger.Core
{
    public interface ILogWriter
    {
        void WriteLog(LogBaseData log);
    }
}