//using LogViewer.Services.Model;
using System.Net.Http;

namespace LogViewer.Services
{
    public interface IMvcRestClientHandler
    {
        HttpResponseMessage RequestForBackup(string requestUri, object requestobject);

        HttpResponseMessage GetArchiveLog(string requestUri, object requestobject);

        HttpResponseMessage RequestForRestore(string requestUri, object requestobject);

        HttpResponseMessage RequestForUnload(string requestUri, object requestobject);

        HttpResponseMessage RequestForStats(string requestUri);
    }
}