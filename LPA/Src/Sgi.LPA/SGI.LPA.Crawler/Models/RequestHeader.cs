using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGI.LPA.Crawler.Models
{
    public class RequestHeader
    {
        public string Client { get; set; }
        public string AuthToken { get; set; }
        public string TransactionId { get; set; }
        public string State { get; set; }
    }
}