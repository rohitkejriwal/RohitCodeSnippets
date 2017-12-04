using System.Collections.Generic;
namespace Sgi.LPA.Common.Models.Chat
{
    public class ChatResponse : ResponseBase
    {
        public string ContextId { get; set; }
        public List<string> Suggestions { get; set; }
        public string ImageURL { get; set; }
        public UserInput UserInput { get; set; }
    }

    public enum UserInput
    {
        Correct,
        NotCorrect,
        None
    }
}