namespace Sgi.LPA.Common.Models.Chat
{
    public class ResponseBase
    {
        public string TransactionId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public ChatResponseType ContentType { get; set; }
        public string SessionId { get; set; }
        public string LinkedChatID { get; set; }
    }

    public enum ChatResponseType
    {
        HTML,
        TEXT,
        IMAGE,
        HYPERLINK,
        TEXTARRAY,
        IMAGEARRAY,
        HYPERLINKARRAY
    }
}