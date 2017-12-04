using IOCInfrastructure;
using Newtonsoft.Json;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Helper;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.Http;
using Sgi.LPA.Common.Models.Indexer;
using Sgi.LPA.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LAP.IndexDBDataChannel
{
    public  class WebCrawlerDataService: IDataCrawlerSource
    {

        private ILogHelper _logHelper;
        private const string _webCrawlerUrlKey = "WebCrawlerUrl";
        private readonly IServiceTransactionData _transactionData;
        private readonly string _webCrawlerUrl;

        public WebCrawlerDataService(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _webCrawlerUrl = ConfigHelper.GetAppSettingValueFromConfig(_webCrawlerUrlKey);
            _transactionData = serviceResolver.GetInstance<IServiceTransactionData>();
        }

        public SearchQueryResonse GetCrawlerData(IndexerModel indexDbModel)
        {
            string response = string.Empty;
            string message = string.Empty;
            string txID = string.Empty;
            string sessionID = string.Empty;


            try
            {
                WebRequest webRequest = WebRequest.Create(_webCrawlerUrl);
 
                var requestHeader = _transactionData.GetRequestHeader();
                if (requestHeader != null)
                {
                    txID = requestHeader.TransactionId;
                    sessionID = requestHeader.SessionId;
                }

                webRequest.ContentType = "application/json";
                webRequest.Method = "POST";
                webRequest.Headers.Add(Constants.HttpHeaderClientKey, Constants.HttpHeaderClientValue);
                webRequest.Headers.Add(Constants.HttpHeaderSessionId, sessionID);
                webRequest.Headers.Add(Constants.HttpHeaderTnxId, txID);
                webRequest.Headers.Add(Constants.HttpHeaderAuthorization, Constants.HttpHeaderAuthorizationValue);
                webRequest.Headers.Add(LPAConsts.HttpHeaderClientStateKey, _transactionData.GetApplicationTransactionStep());


                // add key : LPAConsts.HttpHeaderClientStateKey  
                // value : _txnData.GetApplicationTransactionStep() to the http header.
                
                message = JsonConvert.SerializeObject(indexDbModel);

                var postData = message;

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
                    object obj = JsonConvert.DeserializeObject(response, typeof(MVCResponse));
                    var Response = (MVCResponse)obj;

                    if (Response != null && !string.IsNullOrEmpty(Response.Response))
                    {
                        SearchQueryResonse queryResponse = new SearchQueryResonse();
                        queryResponse.Responses = new List<string>() { Response.Response };
                        queryResponse.ContentType = indexDbModel.ContentType;
                        queryResponse.Status = 0;
                        return queryResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex, "IndexDBSearch", "GetResponseFromWebCrawler", null, this);
            }

            return null;
        }
    }
}
