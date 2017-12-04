using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Common.Models.Chat
{
    public class ChatHistory
    {
        public string ResponseMessage { get; set; }
        public string ContentType { get; set; }
        public string messageType { get; set; } //Query or Response
    }
}
