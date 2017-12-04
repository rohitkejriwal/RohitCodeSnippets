//using LogViewer.Services.Interfaces;
//using LogViewer.Services.Model;
using System.Net.Http;
using System.Net.Http.Headers;

namespace LogViewer.Services
{
    public class MvcRestClientHandler : IMvcRestClientHandler
    {
        private HttpClient _loggerPurgingService;

        public MvcRestClientHandler()
        {
            _loggerPurgingService = new HttpClient();
            _loggerPurgingService.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage RequestForBackup(string requestUri, object requestobject)
        {
            HttpResponseMessage _responseMessage = _loggerPurgingService.PostAsJsonAsync(requestUri, requestobject).Result;
            return _responseMessage;
        }

        public HttpResponseMessage GetArchiveLog(string requestUri, object requestobject)
        {
            HttpResponseMessage _responseMessage = _loggerPurgingService.PostAsJsonAsync(requestUri, requestobject).Result;
            return _responseMessage;
        }

        public HttpResponseMessage RequestForRestore(string requestUri, object requestobject)
        {
            HttpResponseMessage _responseMessage = _loggerPurgingService.PostAsJsonAsync(requestUri, requestobject).Result;
            return _responseMessage;
        }

        public HttpResponseMessage RequestForUnload(string requestUri, object requestobject)
        {
            HttpResponseMessage _responseMessage = _loggerPurgingService.PostAsJsonAsync(requestUri, requestobject).Result;
            return _responseMessage;
        }

        public HttpResponseMessage RequestForStats(string requestUri)
        {
            HttpResponseMessage _responseMessage = _loggerPurgingService.GetAsync(requestUri).Result;
            return _responseMessage;
        }
    }
}