using IOCInfrastructure;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.Models.Chat;
using System;

namespace Sgi.LPA.ChatEngine
{
    public class ResponseFormatter : IResponseFormatter
    {
        private ILogHelper _logHelper;

        public ResponseFormatter(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
        }

        public ChatResponse Format(ChatResponse response)
        {
            _logHelper.LogInfo("Format", "Enter to response formatter", null, this);

            if(response.ContentType == null)
                response.ContentType = ChatResponseType.TEXT;

            _logHelper.LogStep("Format chat response", this);
            return response;
        }
    }
}