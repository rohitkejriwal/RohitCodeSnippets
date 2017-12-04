using IOCInfrastructure;
using IOCInfrastructure.MVC;
using SGI.LPA.Crawler.Models;
using SGI.LPA.Crawler.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SGI.LPA.Crawler.App_Start
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
            //serviceRequestData.SetUser(_resolver.GetInstance<ILPAUAM>());
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
            if (httpContext.Request.Headers[Constants.HttpHeaderClientKey] != null)
            {
                header.Client = httpContext.Request.Headers[Constants.HttpHeaderClientKey];
            }

            if (httpContext.Request.Headers[Constants.HttpHeaderClientStateKey] != null)
            {
                header.State = httpContext.Request.Headers[Constants.HttpHeaderClientStateKey];
            }

            if (httpContext.Request.Headers[Constants.HttpHeaderTnxId] != null)
            {
                header.TransactionId = httpContext.Request.Headers[Constants.HttpHeaderTnxId];
            }
            else
            {
                header.TransactionId = "NTX_" + Guid.NewGuid().ToString();
            }

            if (httpContext.Request.Headers[Constants.HttpHeaderAuthorization] != null)
            {
                header.AuthToken = httpContext.Request.Headers[Constants.HttpHeaderAuthorization];
            }

            return header;
        }
    }
}