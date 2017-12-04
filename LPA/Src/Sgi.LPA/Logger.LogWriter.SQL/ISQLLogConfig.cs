using Logger.Core;

namespace Logger.LogWriter.SQL
{
    public interface ISQLLogConfig : ILogConfig
    {
        string ConnectionString { get; set; }
    }
}