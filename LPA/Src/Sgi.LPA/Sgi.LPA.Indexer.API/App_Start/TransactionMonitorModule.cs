using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.Indexer.Service.Model;
using System;
using System.Diagnostics;
using System.Web;

namespace Sgi.LPA.Indexer.API.App_Start
{
    public class TransactionMonitorModule : MVCTransactionBaseModule
    {
        public override void OnHeaderSent(object sender, EventArgs e)
        {
        }

        public override void OnBeginRequest(object sender, EventArgs e)
        {
            var httpApp = (HttpApplication)sender;
            var serviceRequestData = _resolver.GetInstance<IServiceTransactionData>();
            serviceRequestData.SetRequestHeader(GetRequestHeader(httpApp.Context));
            serviceRequestData.UpdateTransactionStep();

            var timer = new Stopwatch();
            serviceRequestData.SetStopwatch(timer);
            timer.Start();
        }

        public override void OnEndRequest(object sender, EventArgs e)
        {
        }

        private IndexerRequestHeader GetRequestHeader(HttpContext httpContext)
        {
            IndexerRequestHeader header = new IndexerRequestHeader();
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
    }
}