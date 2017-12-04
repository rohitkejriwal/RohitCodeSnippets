using Sgi.LPA.Common.Models.Chat;
using System;

namespace Sgi.LPA.Common.Channel
{
    public interface IDataChannelFactory
    {
        IDataChannel GetDataChannel(Func<string, ChatMessage> chatMessage);
    }
}