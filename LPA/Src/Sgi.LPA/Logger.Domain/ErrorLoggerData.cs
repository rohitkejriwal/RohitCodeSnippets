namespace Logger.Domain
{
    public class ErrorLoggerData : WarningLoggerData
    {
        public string ErrorSource { get; set; }
        public string ErrorClass { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }

        public ErrorLoggerData()
        {
            Level = LogLevels.Error;
        }
    }
}