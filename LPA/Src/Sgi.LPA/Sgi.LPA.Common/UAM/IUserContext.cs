using Sgi.LPA.Common.Models.PAM;

namespace Sgi.LPA.Common.UAM
{
    public interface IUserContext
    {
        LPAUser GetUser(string authToken);

        string GetUserAuthToken(string userName, string phone);
    }
}
