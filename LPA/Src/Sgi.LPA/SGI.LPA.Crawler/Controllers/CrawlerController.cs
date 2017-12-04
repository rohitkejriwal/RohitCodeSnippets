using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using IOCInfrastructure;
using IOCInfrastructure.MVC;
using SGI.LPA.Crawler.Service;
using SGI.LPA.Crawler.Service.Models;

namespace SGI.LPA.Crawler.Controllers
{
    [WebApiFilterIOCBaseAttribute]
    [System.Web.Http.RoutePrefix("api/crawler")]
    public class CrawlerController : ApiController
    {
        private ILogHelper _logHelper;
        private IServiceTransactionData _serviceRequestData;
        private ICrawlerService _crawlerService;
        public CrawlerController(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
            _crawlerService = serviceResolver.GetInstance<ICrawlerService>("WebCrawlerService");
        }

        [System.Web.Http.HttpGet]        
        public HttpResponseMessage Get(IndexerModel indexer)
        {
            return MvcUtility.GetSuccessStatusMessageWithData<string>("Its working :)");
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("crawl")]
        public HttpResponseMessage Crawl(IndexerModel indexer)
        {
            try
            {
                _logHelper.LogDebug("CrawlerController.Post", "Enter to Crawler", null, this);
                _logHelper.LogStep("Enter to CrawlerController.post method", this);

                var response = _crawlerService.GetImageBytesFromWebsite(indexer);
                _logHelper.LogStep("Exit from CrawlerController.post method", this);

                return MvcUtility.GetSuccessStatusMessageWithData<string>(response);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(ex, "CrawlerController", "Crawl", null, this);
                return MvcUtility.GetSuccessStatusMessageWithData<string>(string.Empty);
            }
        }
	}
}