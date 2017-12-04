using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGI.LPA.Crawler.Utilities
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
        public static string TransactionStepKey = "txnSteps";
        public static string StopwatchKey = "stopwatch";
        public const string TransactionClientStateKey = "clientstatekey";
        public const string TransactionStepDurationKey = "txnStepDuration";

    }
}