using IOCInfrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sgi.LAP.AzureSearchDataChannel;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.NLP;
using System.Collections.Generic;

namespace AzureSearchDataChannel.Test
{
    [TestClass]
    public class AzureSearchTest
    {
        public AzureSearchTest()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (ServiceContainer container = new ServiceContainer())
            {
                DependencyHelper.RegisterDepenency(container);

                AzureSearch azureSearch = new AzureSearch(container.GetResolver());

                Intent intent = new Intent()
                {
                    query = "How to play Lotto6/49",
                    topScoringIntent = new ChildIntent()
                    {
                        intent = "GameDetails",
                        score = 0.27974084f
                    }
                };

                List<Entity> testEntities = new List<Entity>();
                testEntities.Add(new Entity()
                {
                    entity = "lotto6 / 49",
                    type = "GameName",
                    startIndex = 12,
                    endIndex = 20,
                    score = 0.976066947f
                });

                SearchQueryResonse response = azureSearch.SendQuery(intent);
            }
        }
    }
}