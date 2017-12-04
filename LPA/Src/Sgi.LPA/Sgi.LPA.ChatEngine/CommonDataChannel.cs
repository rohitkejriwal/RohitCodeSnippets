using IOCInfrastructure;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.NLPParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.ChatEngine
{
    public class CommonDataChannel : IDataChannel
    {
        private ILogHelper _logHelper;
        private IServiceTransactionData _serviceRequestData;
        IDataChannel _indexDbDataChannel; 
        public CommonDataChannel(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
            _indexDbDataChannel = serviceResolver.GetInstance<IDataChannel>(LPAConsts.DataChannelIndexDBSearchKey);
        }
        public Common.Models.Chat.SearchQueryResonse SendQuery(Common.NLP.Intent intent)
        {
            _logHelper.LogStep("Enter to CommonDataChannel.SendQuery method", this);

            var chatMessage = _serviceRequestData.Get <ChatMessage>(LPAConsts.ServiceRequestDataChatMessageKey);
            SearchQueryResonse response = new SearchQueryResonse()
            {
                ContentType = Common.Models.Enums.ContentTypes.text
            };
            if (intent.topScoringIntent.intent == LPAConsts.LUISNoneIntent || intent.topScoringIntent.score < 0.6)
            {
                intent.topScoringIntent.intent = LPAConsts.LUISNoneIntent;
                response.Responses = GetNoneIntentResponse();
                response.IsSuggestionRequired = true;
                response.Status = LPAConsts.SearchQueryResonseSuccessStatus;
            }
            else if (intent.dialog != null && intent.dialog.status == DialogStatus.Question.ToString()
                && !string.IsNullOrEmpty(intent.dialog.prompt) && !string.IsNullOrEmpty(intent.dialog.contextId))
            {
                if (intent.dialog.contextId == chatMessage.ContextId)
                {   
                    response.Responses = GetNoneIntentResponse();
                }
                else
                {
                    response.Responses.Add(string.Join(". ", intent.dialog.prompt));
                    response.ContextId = intent.dialog.contextId;
                    response.IsSuggestionRequired = true;
                }
                response.Status = LPAConsts.SearchQueryResonseSuccessStatus;
            }
            _logHelper.LogStep("Exit from CommonDataChannel.SendQuery method", this);
            return response;
        }

        private List<string> GetNoneIntentResponse()
        {
            var indexDbResponse = _indexDbDataChannel.SendQuery(new Sgi.LPA.Common.NLP.Intent() { topScoringIntent = new Common.NLP.ChildIntent() { intent = LPAConsts.LUISNoneIntent } });
            return indexDbResponse.Responses;
                 
        }
    }
}
