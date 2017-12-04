using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using SGI.LPA.LuisTrainer.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using SGI.LPA.LuisTrainer.Interfaces;
using System.Net;
using System.IO;

namespace SGI.LPA.LuisTrainer
{
    public class LUISAPIService : ILuisAPIService
    {
        

        public string GetLUISEntities(string appId, string OcpApimSubscriptionKey)
        {
            string response = string.Empty;

            var uri = string.Format("{0}/{1}/entities", Constants.luisBaseUrl, appId);

            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webRequest.Headers.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);

            try
            {
                WebResponse webResp = webRequest.GetResponse();

                using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch
            {
                response = string.Empty;
            }
            return response;
        }

        public string AddLabel(string appId, string OcpApimSubscriptionKey, string utterance, string intentName, string entityType, int startToken, int endToken)
        {
            string response = string.Empty;

            var uri = string.Format("{0}/{1}/example", Constants.luisBaseUrl, appId);

            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            webRequest.Headers.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);

            try
            {
                UtteranceLabel uttr = new UtteranceLabel()
                {
                    ExampleText = utterance,
                    SelectedIntentName = intentName,
                    EntityLabels = new List<EntityLabel>()
                {
                    new EntityLabel()
                    {
                        EntityType = entityType,
                        StartToken = startToken,
                        EndToken = endToken
                    }
                    }
                };

                var postData = JsonConvert.SerializeObject(uttr);

                var data = Encoding.ASCII.GetBytes(postData);

                webRequest.ContentLength = data.Length;

                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                WebResponse webResp = webRequest.GetResponse();

                using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch
            {
                response = string.Empty;
            }
            return response;
        }

        public string Train(string appId, string OcpApimSubscriptionKey)
        {
            string response = string.Empty;

            var uri = string.Format("{0}/{1}/train", Constants.luisBaseUrl, appId);

            WebRequest webRequest = WebRequest.Create(uri);
            //webRequest.ContentType = "application/json";
            webRequest.Method = "GET";
            webRequest.Headers.Add("Ocp-Apim-Subscription-Key", OcpApimSubscriptionKey);

            try
            {
                WebResponse webResp = webRequest.GetResponse();

                using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch
            {
                response = string.Empty;
            }
            return response;
        }
    }
}
