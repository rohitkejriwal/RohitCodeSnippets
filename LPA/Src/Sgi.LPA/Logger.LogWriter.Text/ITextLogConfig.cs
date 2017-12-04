using Logger.Core;

namespace Logger.LogWriter.Text
{
    public interface ITextLogConfig : ILogConfig
    {
        string FilePath { get; set; }
    }
}