using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class ChatMessage
    {
        public string Message { get; set; }
        public string ContextId { get; set; }
        public string LinkedChatId { get; set; }
    }
}