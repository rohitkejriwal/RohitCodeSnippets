namespace Logger.Domain.Model
{
    public class TransactionDetailModel
    {
        public string TransactionId { get; set; }
        public string User { get; set; }
        public string Ip { get; set; }
        public string ClientInfo { get; set; }
    }
}