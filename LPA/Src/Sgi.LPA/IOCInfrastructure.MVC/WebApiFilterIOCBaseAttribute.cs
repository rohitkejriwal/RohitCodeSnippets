using System.Web.Http.Controllers;

namespace IOCInfrastructure.MVC
{
    public class WebApiFilterIOCBaseAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        [IOCDependencyAttribute]
        public IServiceResolver ServiceResolver { get; set; }

        public WebApiFilterIOCBaseAttribute()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IServiceTransactionData serviceRequestData = ServiceResolver.GetInstance<IServiceTransactionData>();
            ILogHelper logHelper = ServiceResolver.GetInstance<ILogHelper>();

            //if (HttpContext.Current.Session != null && HttpContext.Current.Session[Consts.TransactionIdKey] != null)
            //{
            //    var txnData = serviceRequestData.Get(Consts.TransactionDataKey);
            //    if (txnData == null)
            //    {
            //        serviceRequestData.SetServiceTransactionData(HttpContext.Current.Session[Consts.TransactionIdKey].ToString(), this);
            //    }
            //}
            //else
            //{
            //    actionContext.Response = new HttpResponseMessage()
            //{
            //    Content = new JsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(new
            //    {
            //        status = HttpStatusCode.Forbidden,
            //        message = "Invalid Session",
            //        error = "Invalid Session"
            //    })),
            //    StatusCode = HttpStatusCode.Forbidden
            //};
            //    //log error
            //    Dictionary<string, object> errorData = new Dictionary<string, object>();
            //    errorData["HttpDetails"] = actionContext.Request.GetHttpRequestDetails();
            //    logHelper.LogError(new HttpException((int)HttpStatusCode.Forbidden, "Invalid Session", (int)HttpStatusCode.Forbidden),
            //    this.GetType().Name, MethodBase.GetCurrentMethod().Name, errorData, this);
            //}
            base.OnActionExecuting(actionContext);
        }
    }
}