using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IOCInfrastructure;
using TestHelper;
using Sgi.LPA.NLPParser;
using Sgi.LPA.Common.NLP;
namespace LUIS.Test
{
    [TestClass]
    public class LUSTest
    {
        [TestMethod]
        public void TestLusiBasic()
        {
            using (ServiceContainer container = new ServiceContainer())
            {
                container.AddBasicDependency();
                container.AddLUISDependency();
                var lusi = container.GetResolver().GetInstance<Luis>();

                string query = "How to play Lotto6/49";

                Intent result = lusi.GetNLPResponse(query, null);

                Assert.IsNotNull(result, "Intent result should not be null");
                Assert.AreNotEqual(result.intents.Length, 0, "Expected data in intents");

            }
        }
    }
}
