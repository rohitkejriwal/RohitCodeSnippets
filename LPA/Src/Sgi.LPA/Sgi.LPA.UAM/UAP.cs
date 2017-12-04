using IOCInfrastructure;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.Common.Utilities;

namespace Sgi.LPA.UAM
{
    public class UAP : ILPAUAM
    {
        private readonly IUserContext _userContext;

        public UAP(IServiceResolver serviceResolver)
        {
            _userContext = new UserContext(serviceResolver);
        }

        public Common.Models.PAM.LPAUser GetUser(string authToken)
        {
            return _userContext.GetUser(authToken);
            //return new Common.Models.PAM.LPAUser() { UserId = "Khanin", DataChannels = new System.Collections.Generic.List<string>() { "indexdbsearch", "azuresearch" } };
            //return new Common.Models.PAM.LPAUser() { UserId = "Khanin", DataChannels = new System.Collections.Generic.List<string>() { LPAConsts.DataChannelCommonKey, LPAConsts.DataChannelIndexDBSearchKey},Jurisdiction  = "ALC" };
        }

        public string GetUserAuthToken(string userName, string phone)
        {
            return _userContext.GetUserAuthToken(userName, phone);
        }
    }
}