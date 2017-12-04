using IOCInfrastructure;
using Sgi.LPA.Common.AI;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.Enums;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.NLP;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.NLPParser;
using System;
using System.Collections.Generic;

namespace Sgi.LPA.ChatEngine
{
    public class LPAChat : IChatEngine
    {
        private ILogHelper _logHelper;
        private IServiceTransactionData _serviceRequestData;
        private IAIEngine _aiEngine;
        private INLPParser _nlpPParser;
        private IResponseFormatter _responseFormatter;


        public LPAChat(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();

            _aiEngine = serviceResolver.GetInstance<IAIEngine>();
            _nlpPParser = serviceResolver.GetInstance<INLPParser>();
            _responseFormatter = serviceResolver.GetInstance<IResponseFormatter>();
        }

        public ChatResponse Chat(ChatMessage message)
        {

            _logHelper.LogStep("Enter to ChatEngine.Chat method", this);
            ChatResponse response = null;
            SearchQueryResonse queryResponse = null;
            _serviceRequestData.Set(LPAConsts.ServiceRequestDataChatMessageKey, message);

            // Get LUIS Intent data
            var intent = _nlpPParser.GetNLPResponse(message.Message, message.ContextId);

            if (intent.intents != null)
            {
                // Find Proper Data Channel
                foreach (var dataChannel in _aiEngine.FindChatChannel(intent))
                {
                    // Send to Source using Data Channel
                    queryResponse = dataChannel.SendQuery(intent);
                    if (queryResponse.Status != LPAConsts.SearchQueryResonseNoneStatus)
                    {
                        break;
                    }
                }

                if (queryResponse != null && queryResponse.Responses != null && queryResponse.Responses.Count > 0)
                {
                    response = new ChatResponse()
                    {
                        ResponseMessage = string.Join(". ", queryResponse.Responses),
                        ContextId = queryResponse.ContextId,
                        Suggestions = (queryResponse.IsSuggestionRequired ? _aiEngine.GetSuggestions(intent) : null)
                    };
                }
                else
                {
                    response = new ChatResponse()
                    {
                        ResponseMessage = string.Empty,
                        ContextId = string.Empty,
                    };
                }

                response.ContentType = (ChatResponseType)Enum.Parse(typeof(ChatResponseType), queryResponse.ContentType.ToString(), true);
            }
            else
            {
                response = new ChatResponse();

                switch(intent.query)
                {
                    case "Like":
                        intent.query = "<i class=\"glyphicon glyphicon-thumbs-up\">";
                        response.ResponseMessage = "Glad you like it";
                        response.ContentType = ChatResponseType.TEXT;
                        response.ContextId = "";
                        break;
                    case "Dislike":
                        intent.query = "<i class=\"glyphicon glyphicon-thumbs-down\">";
                        response.ResponseMessage = "Can you share a bit more info about what you didn't like? I will pass it on so that we can do better next time";
                        response.ContentType = ChatResponseType.TEXT;
                        response.ContextId = "Feedback";
                        response.Suggestions = new List<string>(){"Cancel Feedback"};
                        break;
                    case "Cancel Feedback":
                        response.ResponseMessage = "OK";
                        response.ContentType = ChatResponseType.TEXT;
                        response.ContextId = "";
                        break;
                    default:
                        response.ResponseMessage = "Thanks for explaining the issue";
                        response.ContentType = ChatResponseType.TEXT;
                        response.ContextId = "";
                        break;
                }
            }
            
            response = _responseFormatter.Format(response);

            _aiEngine.SaveChat(intent, response);

            _logHelper.LogStep("Exit from LAPChat.Chat method", this);
            return response;
        }

        public List<ChatHistory> ChatHistory()
        {
            _logHelper.LogStep("Enter to ChatEngine.ChatHistory method", this);
            return _aiEngine.GetChatHistory();
        }

        public List<ChatHistory> GetUserChatHistory(LPAUser user)
        {
            _logHelper.LogStep("Enter to ChatEngine.ChatHistory method", this);
            return _aiEngine.GetUserChatHistory(user);
        }
    }
}