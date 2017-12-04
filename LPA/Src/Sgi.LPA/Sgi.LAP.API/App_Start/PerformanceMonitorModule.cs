using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Common.Models.Http;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.Common.Utilities;
using System;
using System.Diagnostics;
using System.Web;

namespace Sgi.LAP.API.App_Start
{
    public class PerformanceMonitorModule : MVCTransactionBaseModule
    {
        public override void OnHeaderSent(object sender, EventArgs e)
        {
        }

        // Record the time of the begin request event.
        public override void OnBeginRequest(Object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            var serviceRequestData = _resolver.GetInstance<IServiceTransactionData>();

            serviceRequestData.SetRequestHeader(GetRequestHeader(httpApp.Context));
            serviceRequestData.SetUser(_resolver.GetInstance<ILPAUAM>());

            serviceRequestData.Set(LPAConsts.TransactionClientStateKey, GetClientStateKey(httpApp.Context));         
            serviceRequestData.UpdateTransactionStep();

            var timer = new Stopwatch();
            serviceRequestData.SetStopwatch(timer);
            timer.Start();
        }

        public override void OnEndRequest(Object sender, EventArgs e)
        {
            var serviceRequestData = _resolver.GetInstance<IServiceTransactionData>();
            var timer = serviceRequestData.GetStopwatch();
            _logHelper.LogStep(string.Format("Request end. URL {0}", ((HttpApplication)sender).Context.Request.RawUrl), this);
        }

        public void Dispose()
        {
            /* Not needed */
        }

        private RequestHeader GetRequestHeader(HttpContext httpContext)
        {
            RequestHeader header = new RequestHeader();
            if (httpContext.Request.Headers[LPAConsts.HttpHeaderClientKey] != null)
            {
                header.Client = httpContext.Request.Headers[LPAConsts.HttpHeaderClientKey];
            }

            if (httpContext.Request.Headers[LPAConsts.HttpHeaderSessionId] != null)
            {
                header.SessionId = httpContext.Request.Headers[LPAConsts.HttpHeaderSessionId];
            }

            if (httpContext.Request.Headers[LPAConsts.HttpHeaderTnxId] != null)
            {
                header.TransactionId = httpContext.Request.Headers[LPAConsts.HttpHeaderTnxId];
            }
            else
            {
                header.TransactionId = "NTX_" + Guid.NewGuid().ToString();
            }

            if (httpContext.Request.Headers[LPAConsts.HttpHeaderAuthorization] != null)
            {
                header.AuthToken = httpContext.Request.Headers[LPAConsts.HttpHeaderAuthorization];
            }

            return header;
        }
        private string GetClientStateKey(HttpContext httpContext)
        {
            if (httpContext.Request.Headers[LPAConsts.HttpHeaderClientStateKey] != null)
            {
                return httpContext.Request.Headers[LPAConsts.HttpHeaderClientStateKey];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}