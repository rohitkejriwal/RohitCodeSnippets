using Microsoft.VisualStudio.TestTools.UnitTesting;
using SGI.LPA.LuisTrainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.LuisTrainer.Tests
{
    [TestClass()]
    public class LuisTrainerTests
    {
        [TestMethod()]
        public void GetLUISEntitiesTestPass()
        {
            LuisTrainer trainer = new LuisTrainer();
            var response = trainer.GetLUISEntities(Constants.AppId, Constants.OcpApimSubscriptionKey);

            if(string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetLUISEntitiesTestFail()
        {
            LuisTrainer trainer = new LuisTrainer();
            var response = trainer.GetLUISEntities("TestAppId", Constants.OcpApimSubscriptionKey);

            if (!string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void AddLabelTestPass()
        {
            LuisTrainer trainer = new LuisTrainer();
            string utterance, intentName, entityType, gameName;
            int startToken, endToken;

            gameName = "Poker Lotto";
            utterance = string. Format("How to play {0}?", gameName);
            intentName = "GamePlayStyle";
            entityType = "GameName";
            startToken = utterance.IndexOf(gameName);
            endToken = startToken + gameName.Length;

            var response = trainer.AddLabel(Constants.AppId, Constants.OcpApimSubscriptionKey, utterance, intentName, entityType, startToken, endToken);

            if (string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void AddLabelTestFail()
        {
            LuisTrainer trainer = new LuisTrainer();
            string utterance, intentName, entityType, gameName;
            int startToken, endToken;

            gameName = "Poker Lotto";
            utterance = string.Format("How to play {0}?", gameName);
            intentName = "GamePPlayStyle";
            entityType = "GameName";
            startToken = utterance.IndexOf(gameName);
            endToken = startToken + gameName.Length;

            var response = trainer.AddLabel(Constants.AppId, Constants.OcpApimSubscriptionKey, utterance, intentName, entityType, startToken, endToken);

            if (!string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void TrainTestPass()
        {
            LuisTrainer trainer = new LuisTrainer();

            var response = trainer.Train(Constants.AppId, Constants.OcpApimSubscriptionKey);

            if (string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void TrainTestFail()
        {
            LuisTrainer trainer = new LuisTrainer();

            var response = trainer.Train("TestAppId", Constants.OcpApimSubscriptionKey);

            if (!string.IsNullOrEmpty(response))
            {
                Assert.Fail();
            }
        }
    }
}