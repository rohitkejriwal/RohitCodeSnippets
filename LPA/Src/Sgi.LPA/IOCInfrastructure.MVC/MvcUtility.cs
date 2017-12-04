using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace IOCInfrastructure.MVC
{
    public static class MvcUtility
    {
        private const int SuccessMessageStatusCode = 0;
        private const string SuccessMessage = "Success";
        private const int DefaultErrorMessageCode = 500;

        public static HttpResponseMessage GetSuccessStatusMessageWithData<T>(T messageData)
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = SuccessMessageStatusCode, Message = SuccessMessage, Response = messageData }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.OK
            };
        }

        public static HttpResponseMessage GetSuccessStatusMessage()
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = SuccessMessageStatusCode, Message = SuccessMessage }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.OK
            };
        }

        public static HttpResponseMessage GetFailStatusMessage(string errorMessage)
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = DefaultErrorMessageCode, Message = "fail", Error = errorMessage }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public static HttpResponseMessage GetFailStatusMessage(string messageText, string errorMessageDetails, int errorStatusCode = DefaultErrorMessageCode)
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = errorStatusCode, Message = messageText, Error = errorMessageDetails }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public static HttpResponseMessage GetInternalServerErrorMessage()
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = HttpStatusCode.InternalServerError, Message = HttpStatusCode.InternalServerError.ToString() }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }

        public static HttpResponseMessage GetSuccessCreateSessionMessage(string sessionId)
        {
            JToken json = (JToken)Newtonsoft.Json.JsonConvert.DeserializeObject(Newtonsoft.Json.JsonConvert.SerializeObject(new { Status = SuccessMessageStatusCode, Message = SuccessMessage, SessionId = sessionId }));
            return new HttpResponseMessage()
            {
                Content = new JsonContent(json),
                StatusCode = HttpStatusCode.OK
            };
        }

        public static string GetHttpRequestDetails(this HttpRequestMessage httpRequest)
        {
            StringBuilder requestLog = new StringBuilder();

            try
            {
                requestLog.AppendLine(httpRequest.ToString());
                requestLog.AppendLine(httpRequest.Content.ReadAsStringAsync().Result);
                // TODO: need to add post body data

                return requestLog.ToString();
            }
            catch (Exception ex)
            {
                //  suppress this exception because this method is used for logging error.
                requestLog.Append(string.Format("Error:{0}", ex.Message));
            }
            return requestLog.ToString();
        }
    }
}