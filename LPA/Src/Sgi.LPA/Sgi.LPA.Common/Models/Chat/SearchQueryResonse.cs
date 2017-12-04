using Sgi.LPA.Common.Models.Enums;
using Sgi.LPA.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Common.Models.Chat
{
    public class SearchQueryResonse
    {
        public int Status { get; set; }
        public List<string> Responses { get; set; }
        public ContentTypes ContentType { get; set; }
        public string ContextId { get; set; }
        public bool IsSuggestionRequired { get; set; }
        public SearchQueryResonse()
        {
            Responses = new List<string>();
            Status = LPAConsts.SearchQueryResonseNoneStatus;
            ContextId = string.Empty;
        }
    }
}
