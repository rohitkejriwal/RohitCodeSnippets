using IOCInfrastructure;
using Logger.Core;
using Logger.LogWriter.Text;
using Sgi.LAP.AzureSearchDataChannel;
using Sgi.LPA.AI;
using Sgi.LPA.ChatEngine;
using Sgi.LPA.Common.AI;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.NLP;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.NLPParser;
using Sgi.LPA.UAM;

namespace AzureSearchDataChannel.Test
{
    internal static class DependencyHelper
    {
        public static void RegisterDepenency(IServiceRegister container)
        {
            var config = new TextLogConfig();
            container.RegisterInstance<ILogWriter>(new TextLogWriter(config), ServiceLifetime.Single);
            container.RegisterInstance<ILogConfig>(config, ServiceLifetime.Single);
            container.Register<ILogHelper, LogWriteHelper>();

            container.Register<INLPParser, Luis>();
            container.Register<IChatEngine, LPAChat>();
            container.Register<ILPAUAM, UAP>();
            container.Register<IAIEngine, AIEngine>();
            container.Register<IDataChannel, AzureSearch>("azuresearch");
        }
    }
}