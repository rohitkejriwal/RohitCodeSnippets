using Logger.Core;
using Logger.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace IOCInfrastructure.MVC
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        [IOCDependencyAttribute]
        public IServiceResolver ServiceResolver { get; set; }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                ILogHelper logHelper = ServiceResolver.GetInstance<ILogHelper>();
                Dictionary<string, object> errorData = new Dictionary<string, object>();
                errorData["HttpDetails"] = actionExecutedContext.Request.GetHttpRequestDetails();
                logHelper.LogError(actionExecutedContext.Exception, "CustomExceptionFilter", "OnException", errorData, this);
                actionExecutedContext.Response = MvcUtility.GetInternalServerErrorMessage();
            }
            catch (Exception ex)
            {
                if (ServiceResolver != null)
                {
                    ILogger logger = ServiceResolver.GetInstance<ILogger>();
                    Dictionary<string, object> errorData = new Dictionary<string, object>();
                    logger.Error(new ErrorLoggerData()
                    {
                        ErrorMessage = ex.Message,
                        LoggerName = "CustomExceptionFilter",
                        MethodName = "OnException",
                        Message = actionExecutedContext.Exception.Message,
                        InnerException = ex.InnerException != null ? ex.InnerException.ToString() : "",
                    });
                }
                actionExecutedContext.Response = MvcUtility.GetInternalServerErrorMessage();
            }
            base.OnException(actionExecutedContext);
        }
    }
}