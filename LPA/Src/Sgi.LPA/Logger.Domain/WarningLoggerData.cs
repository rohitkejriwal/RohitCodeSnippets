namespace Logger.Domain
{
    public class WarningLoggerData : InfoLoggerData
    {
        public string MachineName { get; set; }
        public string User { get; set; }
        public string ClientInfo { get; set; }

        public WarningLoggerData()
        {
            Level = LogLevels.Error;
        }
    }
}