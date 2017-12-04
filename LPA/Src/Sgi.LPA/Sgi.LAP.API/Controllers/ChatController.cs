using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.Common.Utilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace Sgi.LAP.API.Controllers
{
    [CustomExceptionFilter]
    [WebApiFilterIOCBaseAttribute]
    public class ChatController : ApiController
    {
        private IChatEngine _chatEngine;
        private ILogHelper _logHelper;
        private IServiceTransactionData _serviceRequestData;
        private readonly ILPAUAM _userManagement;

        public ChatController(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _chatEngine = serviceResolver.GetInstance<IChatEngine>();
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
            _userManagement = serviceResolver.GetInstance<ILPAUAM>();
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(string authToken)
        {
            _logHelper.LogDebug("ChatController.Get", "Enter to GetChatHistory", null, this);
            _logHelper.LogStep("Enter to ChatController.get method", this);

            LPAUser user = _userManagement.GetUser(authToken);
            
            var response = _chatEngine.GetUserChatHistory(user);

            _logHelper.LogStep("Exit from ChatController.get method", this);
            return MvcUtility.GetSuccessStatusMessageWithData<List<ChatHistory>>(response);
        }

        /// <summary>
        /// Posts the specified chat message.
        /// </summary>
        /// <param name="chatMessage">The chat message.</param>
        [HttpPost]
        public HttpResponseMessage Post(ChatMessage chatMessage)
        {
            _logHelper.LogDebug("ChatController.Post", "Enter to Chat", null, this);
            _logHelper.LogStep("Enter to ChatController.post method", this);

            if(Request.Headers.Contains("AuthToken"))
            {
                _serviceRequestData.Set(LPAConsts.AuthToken, ((string[])Request.Headers.GetValues("AuthToken"))[0]);
            }

            var response = _chatEngine.Chat(chatMessage);

            _logHelper.LogStep("Exit from ChatController.post method", this);
            return MvcUtility.GetSuccessStatusMessageWithData<ResponseBase>(response);
        }
    }
}