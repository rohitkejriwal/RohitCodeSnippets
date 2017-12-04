using IOCInfrastructure;
using Logger.Core;
using Sgi.LPA.Common.NLP;
using Sgi.LPA.NLPParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public static class LPADependency
    {
        public static void AddBasicDependency(this ServiceContainer container)
        {
            container.Register<ILogger, MockLogger>();
            container.Register<ILogWriter, MockLogWriter>(ServiceLifetime.Single);
            container.Register<ILogHelper, MockLoggerHelper>();
            
        }
        public static void AddLUISDependency(this ServiceContainer container)
        {
            container.RegisterInstance<INLPParser>(new Luis(container.GetResolver()), ServiceLifetime.Single);
        }

    }
}
