using IOCInfrastructure;
using Newtonsoft.Json;
using Sgi.LPA.Common.NLP;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System;
using Sgi.LPA.Common.Helper;

namespace Sgi.LPA.NLPParser
{
    public class Luis : INLPParser
    {
        const string LUISAPIUrlKey = "LUISAPIUrl";
        const string ApplicationKey = "ApplicationKey";
        const string SubscriptionKey = "SubscriptionKey";
        const string DateTimeEntityType = "builtin.datetime.date";
        const string FeedbackContext = "FEEDBACK";
        const string Like = "like";
        const string Dislike = "dislike";
        const string CancelFeedback = "cancel feedback";
        const string ChatEntity = "chat";

        private readonly ILogHelper _logHelper;
        private readonly string LUISAPIUrl;
        private readonly string LUISApplicationKey;
        private readonly string LUISSubscriptionKey;

        public Luis(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            LUISAPIUrl = ConfigHelper.GetAppSettingValueFromConfig(LUISAPIUrlKey);
            LUISApplicationKey = ConfigHelper.GetAppSettingValueFromConfig(ApplicationKey);
            LUISSubscriptionKey = ConfigHelper.GetAppSettingValueFromConfig(SubscriptionKey);
        }

        public Intent GetNLPResponse(string query, string contextId)
        {
            Intent intent = null;

            if (string.IsNullOrEmpty(contextId))
                contextId = string.Empty;

            if(query.ToUpper() == "<I CLASS=\"GLYPHICON GLYPHICON-THUMBS-UP\">")
            {
                query = "Like";
                contextId = "Feedback";
            }
            else if(query.ToUpper() == "<I CLASS=\"GLYPHICON GLYPHICON-THUMBS-DOWN\">")
            {
                query = "Dislike";
                contextId = "Feedback";
            }

            switch (contextId.ToUpper())
            {
                case FeedbackContext:
                    intent = GetFeedbackResponse(query, contextId);
                    break;
                default:
                    intent = GetLUISResponse(query, contextId);
                    break;
            }
            return intent;
        }

        private Intent GetLUISResponse(string query, string contextId)
        {
            _logHelper.LogStep("Calling Luis", this);
            string response = string.Empty;
            WebRequest webRequest = null;
            Intent intent = null;

            if (!string.IsNullOrEmpty(contextId))
            {
                webRequest = WebRequest.Create(string.Format("{0}{1}?subscription-key={2}&q={3}&verbose=true&contextId={4}", LUISAPIUrl, LUISApplicationKey, LUISSubscriptionKey, HttpUtility.UrlEncode(query), contextId));
            }
            else
            {
                webRequest = WebRequest.Create(string.Format("{0}{1}?subscription-key={2}&q={3}&verbose=true", LUISAPIUrl, LUISApplicationKey, LUISSubscriptionKey, query));
            }
            _logHelper.LogStep("Sending LUIS request", this);
            WebResponse webResp = webRequest.GetResponse();
            _logHelper.LogStep("LUIS response received", this);
            
            using (StreamReader sr = new StreamReader(webResp.GetResponseStream()))
            {
                response = sr.ReadToEnd();
                object obj = JsonConvert.DeserializeObject(response, typeof(Intent));
                intent = (Intent)obj;
            }
            _logHelper.LogStep("Got data from LUIS", this);

            intent = FormatLUISResponse(intent);

            _logHelper.LogStep("LUIS response Intent responded", this);
            return intent;

        }

        private Intent FormatLUISResponse(Intent intent)
        {
            _logHelper.LogStep("Formating LUIS Response", this);
            var entities = intent.entities.Where(_ => _.type == DateTimeEntityType).ToList();

            if (entities.Any())
            {
                foreach (var item in intent.entities)
                {
                    if (item.type == DateTimeEntityType)
                    {
                        item.entity = FormatDate(item.entity);
                    }
                }
            }

            return intent;
        }

        private string FormatDate(string date)
        {
            string[] dateComponents = date.Split('-');
            string year = "", month = "", day = "", weekNumber = "";
            string dates = string.Empty;

            try
            {
                if (dateComponents != null && dateComponents.Length > 0)
                {
                    switch (dateComponents.Length)
                    {
                        case 1:
                            year = dateComponents[0];
                            //dates = GetYear(year);
                            break;
                        case 2:
                            //year = dateComponents[0];
                            //if (dateComponents[1].Contains("W"))
                            //{
                            //    weekNumber = dateComponents[0].Substring(1);
                            //    dates = GetDatesOfWeek(weekNumber, year);
                            //}
                            //else
                            //{
                            //    month = dateComponents[1];
                            //    dates = GetDatesOfMonth(month, year);
                            //}
                            break;
                        default:
                            year = dateComponents[0];
                            month = dateComponents[1];
                            day = dateComponents[2];
                            dates = GetDate(year, month, day);
                            break;
                    }
                }

                return dates;
            }
            catch
            {
                return dates;
            }
        }

        private string GetDate(string year, string month, string day)
        {
            string date = string.Format("{0}-{1}-{2}",GetYear(year),month,day);
            return date;
        }

        private string GetYear(string year)
        {
            if(year.StartsWith("xxxx"))
            {
                return DateTime.Now.Year.ToString();
            }
            else if(year.StartsWith("xx"))
            {
                var yearPart = int.Parse(year.Substring(2));
                var currentYearPart = DateTime.Now.Year % 100;

                if(yearPart <= currentYearPart)
                {
                    return string.Format("{0}{1}", DateTime.Now.Year / 100, yearPart);
                }
                else
                {
                    return string.Format("{0}{1}", (DateTime.Now.Year / 100)-1, yearPart);
                }
            }
            return year;
        }

        private string GetDatesOfMonth(string month, string year)
        {
            throw new System.NotImplementedException();
        }

        private string GetDatesOfWeek(string weekNumber, string year)
        {
            throw new System.NotImplementedException();
        }
    
        private Intent GetFeedbackResponse(string query, string contextId)
        {
            _logHelper.LogStep("Creating Feedback intent", this);
            string response = string.Empty;
            Intent responseIntent = new Intent();

            responseIntent.query = query;
            responseIntent.topScoringIntent = new ChildIntent()
            {
                intent = "Conversation",
                score = 1
            };

            responseIntent.entities = GetFeedbackEntities(query);

            if (query.ToUpper() == "DISLIKE")
            {
                responseIntent.dialog = new Dialog()
                {
                    contextId = "feedback"
                };
            }

            return responseIntent;
        }

        private Entity[] GetFeedbackEntities(string query)
        {
            Entity[] entities = null;

            switch (query.ToUpper())
            {
                case "LIKE":
                case "<I CLASS=\"GLYPHICON GLYPHICON-THUMBS-UP\">":
                    entities = new Entity[]
                                {
                                    new Entity()
                                    {
                                        entity = Like,
                                        type = ChatEntity,
                                        startIndex = 0,
                                        endIndex = 6,
                                        score = 1
                                    }
                                };
                    break;
                case "DISLIKE":
                case "<I CLASS=\"GLYPHICON GLYPHICON-THUMBS-DOWN\">":
                    entities = new Entity[]
                                {
                                    new Entity()
                                    {
                                        entity = Dislike,
                                        type = ChatEntity,
                                        startIndex = 0,
                                        endIndex = 3,
                                        score = 1
                                    }
                                };
                    break;
                case "CANCEL FEEDBACK":
                    entities = new Entity[]
                                {
                                    new Entity()
                                    {
                                        entity = CancelFeedback,
                                        type = ChatEntity,
                                        startIndex = 0,
                                        endIndex = 14,
                                        score = 1
                                    }
                                };
                    break;
                default:
                    entities = new Entity[]
                                {
                                    new Entity()
                                    {
                                        entity = "Feedback Comment",
                                        type = ChatEntity,
                                        startIndex = 0,
                                        endIndex = 14,
                                        score = 1
                                    }
                                };
                    break;
            }

            return entities;
        }
    }
}