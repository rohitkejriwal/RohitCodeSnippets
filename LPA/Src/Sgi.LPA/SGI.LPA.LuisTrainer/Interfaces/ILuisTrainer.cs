using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.LuisTrainer.Interfaces
{
    public interface ILuisTrainer
    {
        string GetLUISEntities(string appId, string OcpApimSubscriptionKey);

        string AddLabel(string appId, string OcpApimSubscriptionKey, string utterance, string intentName, string entityType, int startToken, int endToken);

        string Train(string appId, string OcpApimSubscriptionKey);
    }
}
