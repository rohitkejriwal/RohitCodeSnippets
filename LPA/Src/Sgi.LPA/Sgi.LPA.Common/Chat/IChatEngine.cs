using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.PAM;
using System.Collections.Generic;

namespace Sgi.LPA.Common.Chat
{
    public interface IChatEngine
    {
        ChatResponse Chat(ChatMessage message);

        List<ChatHistory> ChatHistory();

        List<ChatHistory> GetUserChatHistory(LPAUser user);
    }
}