using System;

namespace Logger.Domain
{
    public class LogBaseData
    {
        public string _id { get; set; }
        public string TransactionId { get; set; }
        public string SessisonId { get; set; }
        public string LoggerName { get; set; }
        public string Message { get; set; }
        public int Level { get; protected set; }
        public double CreatedOn { get; set; }

        public LogBaseData()
        {
            TransactionId = string.Empty;
            LoggerName = string.Empty;
            Message = string.Empty;
            CreatedOn = DomainUtility.ConvertToUnixTimestamp(DateTime.UtcNow);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", TransactionId, LoggerName, Message);
        }
    }
}