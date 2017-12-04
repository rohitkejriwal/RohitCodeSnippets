namespace Sgi.LPA.Common.Models.Http
{
    public class RequestHeader
    {
        public string Client { get; set; }
        public string AuthToken { get; set; }
        public string TransactionId { get; set; }
        public string SessionId { get; set; }
    }
}