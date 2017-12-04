using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.NLP;
using System.Collections.Generic;

namespace Sgi.LPA.Common.AI
{
    public interface IAIEngine
    {
        /// <summary>
        ///  find most appropriate auto-correct sentence
        /// </summary>
        /// <returns></returns>
        string JustifyAutoCorrect();
        List<IDataChannel> FindChatChannel(Intent chatIntent);
        IDataChannel FindChatChannel(string channelName);
        void SaveChat(Intent intent, IDataChannel dataChannel);
        void SaveChat(Intent intent, ChatResponse response);
        List<string> GetSuggestions(Intent intent);
        List<ChatHistory> GetChatHistory();
        List<ChatHistory> GetUserChatHistory(LPAUser user);
    }
}