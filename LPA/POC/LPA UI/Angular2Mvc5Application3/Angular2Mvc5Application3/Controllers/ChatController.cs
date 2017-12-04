using Angular2Mvc5Application3.Helpers;
using Models;
using Models.DAL;
using Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System;

namespace Angular2Mvc5Application3.Controllers
{
    public class ChatController : Controller
    {
        private const string chatAPIUrlKey = "chatAPIUrl";
        private const string chatHistoryUrlKey = "chatHistoryUrl";
        private readonly string chatAPIUrl ;
        private readonly string chatHistoryUrl;

        public ChatController()
        {
            chatAPIUrl = ConfigHelper.GetAppSettingValueFromConfig(chatAPIUrlKey);
            chatHistoryUrl = ConfigHelper.GetAppSettingValueFromConfig(chatHistoryUrlKey);
        }

        public ActionResult Index(string authToken)
        {
            Session["authToken"] = authToken;
            return View();
        }


        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public JsonResult GetChatList(string authToken)
        { 
            List<Chat> chatHistory = new List<Chat>();
            string userAuthToken = (string)(Session["authToken"]);

            chatHistory.AddRange(GetChatHistory(userAuthToken));

            return Json(chatHistory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetChatResponse(Chat chat)
        {
            Chat respChat = null;

            string authToken = chat.AuthToken;

            if(string.IsNullOrEmpty(authToken))
                authToken = (string)(Session["authToken"]);

            respChat = GetChatResponse(chat.Message, chat.ContextID, chat.LinkedChatID, authToken);

            if (respChat == null)
            {
                respChat = new Chat()
                {
                    Id = 0,
                    Message = "Sorry! I don't understand you.",
                    Type = "Response",
                    ContentType = "text"

                };
            }
            else if(respChat != null && string.IsNullOrEmpty(respChat.Message))
            {
                respChat.Message = "We didn't find anything relevant now!";
            }

            return Json(new JSONReturnVM<Chat>(respChat, this.ModelState));
        }

        private Chat GetChatResponse(string query, string contextID, string linkedChatID, string authToken)
        {
            string response = string.Empty;
            string message = string.Empty;
            ChatMessage chatMessage = null;
            try
            {
                WebRequest webRequest = WebRequest.Create(chatAPIUrl);

                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("lap-clientid", "LPA");
                webRequest.Headers.Add("lap-sessionid", "111223");
                webRequest.Headers.Add("lap-transactionid", "111199999q");
                webRequest.Headers.Add("Authorization", "Basic a2hhbmluOnBhc3N3ZA==");
                webRequest.Headers.Add("AuthToken", authToken);
                webRequest.Method = "POST";

                if (!string.IsNullOrEmpty(contextID))
                {
                    chatMessage = new ChatMessage
                    {
                        Message = query,
                        ContextId = contextID,
                        LinkedChatId = linkedChatID
                    };
                }
                else
                {
                    chatMessage = new ChatMessage
                    {
                        Message = query,
                        ContextId = "",
                        LinkedChatId = linkedChatID
                    };
                }

                message = JsonConvert.SerializeObject(chatMessage);

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
                    object obj = JsonConvert.DeserializeObject(response, typeof(ChatResponse));
                    var chatResponse = (ChatResponse)obj;

                    if(chatResponse != null)
                    {
                        Chat chat = new Chat
                        {
                            Message = chatResponse.Response.ResponseMessage,
                            Type = "Response",
                            Suggestions = chatResponse.Response.Suggestions,
                            ContextID = chatResponse.Response.ContextId,
                            ContentType = chatResponse.Response.ContentType.ToString(),
                            LinkedChatID = string.IsNullOrEmpty(chatResponse.Response.LinkedChatID) ? null : chatResponse.Response.LinkedChatID.ToString()
                        };
                       
                        return chat;
                    }
                }
            }
            catch
            {
                
            }

            return null;
        }

        private List<Chat> GetChatHistory(string authToken)
        {
            string response = string.Empty;
            List<Chat> chats = new List<Chat>();
            string serializedUser = string.Empty;

            try
            {
                WebRequest webRequest = WebRequest.Create(string.Format("{0}?authToken={1}", chatAPIUrl, authToken));
                webRequest.Method = "GET";
                WebResponse webResp = webRequest.GetResponse();

                using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                    object obj = JsonConvert.DeserializeObject(response, typeof(ChatHistoryResponse));

                    List<ChatHistory> chatHistory = ((ChatHistoryResponse)obj).Response;

                    if (chatHistory != null)
                    {
                        foreach (var item in chatHistory)
                        {
                            var chat = new Chat()
                            {
                                Message = string.IsNullOrEmpty(item.ResponseMessage) ? "Sorry! I don't understand you." : item.ResponseMessage,
                                Type = item.messageType,
                                ContentType = item.ContentType
                            };
                            chats.Add(chat);
                        }
                    }
                }
            }
            catch
            {
                Chat chat = new Chat()
                {
                    Id = 1,
                    Message = "Exception",
                    Type = "Response",
                    ContentType = "text"
                };
                chats.Add(chat);
            }

            chats = FormatChats(chats);

            return chats;
        }

        private List<Chat> FormatChats(List<Chat> chats)
        {
            List<Chat> welcomeChats = new List<Chat>();
            if(chats == null || chats.Count == 0)
            {
                welcomeChats.Add(new Chat()
                {
                    Message ="Hi ! I'm your Lottery Personal Assistant",
                    Type = "Response",
                    ContentType = "text"
                });
                welcomeChats.Add(new Chat()
                {
                    Message = "I can help you with lottery related queries like game information, winners and winning informations, etc",
                    Type = "Response",
                    ContentType = "text"
                });

                return welcomeChats;
            }

            return chats;
        }
    }
}