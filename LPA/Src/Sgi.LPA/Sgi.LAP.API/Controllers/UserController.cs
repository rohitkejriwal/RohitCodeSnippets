using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.UAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sgi.LAP.API.Controllers
{
    public class UserController : ApiController
    {
        private ILPAUAM _userEngine;
        private ILogHelper _logHelper;

        public UserController(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _userEngine = serviceResolver.GetInstance<ILPAUAM>();
        }

        [HttpGet]
        public HttpResponseMessage Get(string userName, string phone)
        {
            _logHelper.LogDebug("ChatController.Get", "Enter to GetUserAuthToken", null, this);
            _logHelper.LogStep("Enter to UserController.get method", this);

            var response = _userEngine.GetUserAuthToken(userName, phone);

            _logHelper.LogStep("Exit from UserController.get method", this);
            return MvcUtility.GetSuccessStatusMessageWithData<string>(response);
        }
    }
}
