using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.NLP;
using System.Collections.Generic;

namespace Sgi.LPA.Common.Chat
{
    public interface IChatContext
    {
        List<IDataChannel> GetCurrentContextChennal();

        void SaveChatHistory(Intent intent);
        void SaveChatHistory(Intent intent, ChatResponse response);
        List<string> GetSuggestions(Intent intent);
        List<UserChat> GetChatHistory();
        List<UserChat> GetUserChatHistory(LPAUser user);
    }
}