using System.Collections.Generic;
namespace Sgi.LPA.Common.Models.PAM
{
    public class LPAUser
    {
        public string _id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Jurisdiction { get; set; }
        public List<string> DataChannels { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string AuthToken { get; set; }
    }
}
