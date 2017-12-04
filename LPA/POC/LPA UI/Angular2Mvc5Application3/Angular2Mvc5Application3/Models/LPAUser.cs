using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class LPAUser
    {
        public string _id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Jurisdiction { get; set; }
        public List<string> DataChannels { get; set; }

        public LPAUser()
        {

        }

        public LPAUser(string userId)
        {
            UserId = userId;
        }
    }
}