using Sgi.LPA.Common.Models.Chat;

namespace Sgi.LPA.Common.Chat
{
    public interface IResponseFormatter
    {
        ChatResponse Format(ChatResponse response);
    }
}