using SGI.LPA.LuisTrainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.LuisTrainer
{
    public class LuisTrainer : ILuisTrainer
    {
        public string GetLUISEntities(string appId, string OcpApimSubscriptionKey)
        {
            return new LUISAPIService().GetLUISEntities(appId, OcpApimSubscriptionKey);
        }

        public string AddLabel(string appId, string OcpApimSubscriptionKey, string utterance, string intentName, string entityType, int startToken, int endToken)
        {
            return new LUISAPIService().AddLabel(appId, OcpApimSubscriptionKey, utterance, intentName, entityType, startToken, endToken);
        }

        public string Train(string appId, string OcpApimSubscriptionKey)
        {
            return new LUISAPIService().Train(appId, OcpApimSubscriptionKey);
        }
    }
}
