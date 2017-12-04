using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.NLP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Common.Chat
{
    public class UserChat
    {
        public string query { get; set; }
        public ChildIntent intent { get; set; }
        public Entity[] entities { get; set; }
        public string userID { get; set; }
        public DateTime logDate { get; set; }
        public string response { get; set; }
        public string responseType { get; set; }
        public bool isCleared { get; set; }
    }
}
