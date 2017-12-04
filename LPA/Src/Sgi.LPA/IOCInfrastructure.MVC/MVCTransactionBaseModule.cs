using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IOCInfrastructure.MVC
{
    public abstract class MVCTransactionBaseModule : IHttpModule
    {
        protected ILogHelper _logHelper;
        protected IServiceResolver _resolver;

        public void Init(HttpApplication httpApp)
        {
            var containerProviderAccessor = httpApp as IContainerProviderAccessor;
            _resolver = containerProviderAccessor.ServiceResolver;
            _logHelper = _resolver.GetInstance<ILogHelper>();

            httpApp.BeginRequest += OnBeginRequest;
            httpApp.EndRequest += OnEndRequest;
            httpApp.PreSendRequestHeaders += OnHeaderSent;
        }

        public abstract void OnHeaderSent(object sender, EventArgs e);

        // Record the time of the begin request event.
        public abstract void OnBeginRequest(Object sender, EventArgs e);

        public abstract void OnEndRequest(Object sender, EventArgs e);

        public void Dispose()
        {
            /* Not needed */
        }
    }
}
