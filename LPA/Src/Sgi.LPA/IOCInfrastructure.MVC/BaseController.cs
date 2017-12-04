using System;
using System.Web.Mvc;

namespace IOCInfrastructure.MVC
{
    public class BaseController : Controller
    {
        protected IServiceTransactionData _serviceRequestData;

        public BaseController(IServiceResolver serviceResolver)
        {
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && Session[Consts.TransactionIdKey] != null)
            {
                var txnData = _serviceRequestData.Get(Consts.TransactionDataKey);
                if (txnData == null)
                {
                    _serviceRequestData.SetServiceTransactionData(Session[Consts.TransactionIdKey].ToString(), this);
                }
            }
            else
            {
                throw new Exception("Invalid Session");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}