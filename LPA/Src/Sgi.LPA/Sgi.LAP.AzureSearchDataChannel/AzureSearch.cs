using IOCInfrastructure;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.NLP;
using Sgi.LPA.Common.Utilities;
using System.IO;
using System.Net;
using System.Web;

namespace Sgi.LAP.AzureSearchDataChannel
{
    public class AzureSearch : IDataChannel
    {
        private ILogHelper _logHelper;
        private IServiceTransactionData _serviceRequestData;

        private string AzureSearchUrl = @"https://khaninlpa.search.windows.net/indexes/my-target-index/docs";
        private string APIKey = "637AD3B4637324374845F520DD173420";
        private string APIVersion = "2015-02-28";

        public AzureSearch(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
        }

        public SearchQueryResonse SendQuery(Intent query)
        {
            try
            {               
                return GetAzureSearchResponse(query);
            }
            catch
            {
                return null;
            }
        }

        private SearchQueryResonse GetAzureSearchResponse(Intent query)
        {
            _logHelper.LogInfo("LAPChat.Chat", "Returning Dummy chat responses", null, this);
            _logHelper.LogStep("Getting response from Azure Search", this);
            string response = string.Empty;
            //WebRequest webRequest = WebRequest.Create(string.Format("{0}?api-version={1}&search=*", AzureSearchUrl, APIVersion, HttpUtility.UrlEncode(query.entities[0].type)));
            WebRequest webRequest = WebRequest.Create(string.Format("{0}?api-version={1}&search=*", AzureSearchUrl, APIVersion));
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add(string.Format("api-key:{0}", APIKey));

            WebResponse webResp = webRequest.GetResponse();

            using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }

            _logHelper.LogStep("Got Azure Search Response", this);
            return new SearchQueryResonse()
            {
                Status = 0,
                Responses = new System.Collections.Generic.List<string>() { response }
            };
        }
    }
}