using Sgi.LPA.Common.Models.PAM;

namespace Sgi.LPA.Common.UAM
{
    public interface ILPAUAM
    {
        LPAUser GetUser(string authToken);

        string GetUserAuthToken(string userName, string phone);
    }
}