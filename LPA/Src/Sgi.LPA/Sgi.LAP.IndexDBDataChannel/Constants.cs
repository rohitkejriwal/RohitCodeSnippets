using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LAP.IndexDBDataChannel
{
    public static class Constants
    {
        public const string HttpHeaderClientKey = "crawler-clientid";
        public const string HttpHeaderSessionId = "crawler-sessionid";
        public const string HttpHeaderTnxId = "crawler-transactionid";
        public const string DIRequestHeaderKey = "requestheaderkey";
        public const string HttpHeaderAuthorization = "Authorization";
        public const string HttpHeaderClientStateKey = "clientstatekey";
        public static string UserKey = "user";

        public const string HttpHeaderClientValue = "LPA";
        public const string HttpHeaderAuthorizationValue = "Basic a2hhbmluOnBhc3N3ZA==";
    }
}
