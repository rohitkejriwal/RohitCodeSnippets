namespace IOCInfrastructure
{
    public class TransactionLoggerData
    {
        public string Component { get; set; }
        public string SessionId { get; set; }
        public string TransactionId { get; set; }
        public string ClientInfo { get; set; }

        public TransactionLoggerData()
        {
            Component = string.Empty;
            SessionId = string.Empty;
            TransactionId = string.Empty;
            ClientInfo = string.Empty;
        }
    }
}