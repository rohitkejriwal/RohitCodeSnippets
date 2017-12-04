using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ResponseBase
    {
        public string TransactionId { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public CharResponseType ContentType { get; set; }
        public string SessionId { get; set; }
        public string ContextId { get; set; }
        public List<string> Suggestions { get; set; }
        public string ImageURL { get; set; }
        public UserInput UserInput { get; set; }
        public string LinkedChatID { get; set; }
    }

    public enum CharResponseType
    {
        HTML,
        TEXT,
        IMAGE,
        HYPERLINK,
        TEXTARRAY,
        IMAGEARRAY,
        HYPERLINKARRAY
    }

    public class ChatResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public ResponseBase Response { get; set; }
    }

    public class ChatHistoryResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<ChatHistory> Response { get; set; }
    }

    public class ChatHistory
    {
        public string ResponseMessage { get; set; }
        public string ContentType { get; set; }
        public string messageType { get; set; } //Query or Response
    }

    public enum UserInput
    {
        Correct,
        NotCorrect,
        None
    }
}