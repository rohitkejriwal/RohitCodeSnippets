using IOCInfrastructure;
using IOCInfrastructure.MVC;
using Sgi.LPA.Indexer.Service;
using Sgi.LPA.Indexer.Service.Model;
using System.Net.Http;
using System.Web.Http;

namespace Sgi.LPA.Indexer.API.Controllers.Api
{
    public class IndexController : ApiController
    {
        private ILogHelper _logHelper;
        private IIndexerService _indexerService;

        public IndexController(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _indexerService = serviceResolver.GetInstance<IIndexerService>();
        }

        [HttpGet]
        public string Get()
        {
            _logHelper.LogDebug("Get", "Get method is working", null, this);
            return "Its working :)";
        }

        [HttpPost]
        public HttpResponseMessage Post(IndexerModel model)
        {
            _logHelper.LogDebug("Post", "Enter to indexer", null, this);
            _logHelper.LogStep("Enter to post chat method", this);
            _indexerService.AddUpdateIndex(model);
            _logHelper.LogStep("Exit from post chat method", this);
            return MvcUtility.GetSuccessStatusMessage();
        }
    }
}