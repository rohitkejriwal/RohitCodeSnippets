using IOCInfrastructure;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.NLP;
using System;
using System.Collections.Generic;
using System.Linq;
using Sgi.Core.DBService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Helper;
using Sgi.LPA.Common.UAM;

namespace Sgi.LPA.ChatStorage
{
    public class ChatContext : IChatContext
    {
        private const string NumberOfSuggestions = "NumberOfSuggestions";

        private IDBRepository _dbService;
        private const string _chatCollectionName = "chat";
        private readonly LPAUser _user = null;
        private readonly int _numberOfSuggestions;
        private readonly IServiceTransactionData _serviceRequestData;
        private readonly ILPAUAM _userManagement;

        public ChatContext(IServiceResolver serviceResolver)
        {
            _dbService = serviceResolver.GetInstance<IDBRepository>(LPAConsts.MongoDBChatRepositoryKey);
            _numberOfSuggestions = Convert.ToInt32(ConfigHelper.GetAppSettingValueFromConfig(NumberOfSuggestions));
            _serviceRequestData = serviceResolver.GetInstance<IServiceTransactionData>();
            _userManagement = serviceResolver.GetInstance<ILPAUAM>();
            string authToken = _serviceRequestData.GetAuthToken();
            _user = _userManagement.GetUser(authToken);
        }

        public void SaveChatHistory(Intent intent)
        {
            intent._id = Guid.NewGuid().ToString();

            SaveChatIntent(intent);
        }

        public void SaveChatHistory(Intent intent, ChatResponse response)
        {
            intent._id = Guid.NewGuid().ToString();

            SaveChat(intent, response);
        }

        public List<Common.Channel.IDataChannel> GetCurrentContextChennal()
        {
            throw new NotImplementedException();
        }

        public List<string> GetSuggestions(Intent intent)
        {
            List<string> response = new List<string>();
            string jurisdiction = _user.Jurisdiction;

            intent._id = Guid.NewGuid().ToString();

            List<FilterCriteria> filters = new List<FilterCriteria>();
            //{
            //    new FilterCriteria()
            //        {
            //            DataType = DataType.StringList.ToString(),
            //            Key = "Jurisdiction",
            //            Operator = Operators.Contains.ToString(),
            //            Value = jurisdiction
            //        }
            //};

            if (intent.topScoringIntent.intent != "None" && intent.topScoringIntent.actions.Any())
            {
                var parameters = (from parameter in intent.topScoringIntent.actions[0].parameters
                                  where parameter.value == null
                                  select parameter).ToList();

                foreach (var item in parameters)
                {
                    filters.Add(new FilterCriteria()
                    {
                        DataType = DataType.EntityType.ToString(),
                        Key = "intent.entities.type",
                        Operator = Operators.Contains.ToString(),
                        Value = item.type
                    });
                }
            }
            else if(intent.topScoringIntent.intent == "None" )
            {

                foreach (var item in intent.entities)
                {
                    filters.Add(new FilterCriteria()
                    {
                        DataType = DataType.EntityType.ToString(),
                        Key = "intent.entities.type",
                        Operator = Operators.Contains.ToString(),
                        Value = item.type
                    });
                }
            }

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "userID",
                Operator = Operators.EqualTo.ToString(),
                Value = (string)_serviceRequestData.Get(LPAConsts.AuthToken)
            });

            var chats = _dbService.GetNSortedData(_chatCollectionName, filters, 100, "logDate", false);

            response = FormatSuggestion(chats, intent);            
            return response.Distinct().ToList();
        }

        public List<UserChat> GetChatHistory()
        {
            List<UserChat> response = new List<UserChat>();
            string jurisdiction = _user.Jurisdiction;
            string authToken = (string)_serviceRequestData.Get(LPAConsts.AuthToken);
            LPAUser user = _userManagement.GetUser(authToken);

            List<FilterCriteria> filters = new List<FilterCriteria>();
            //{
            //    new FilterCriteria()
            //        {
            //            DataType = DataType.StringList.ToString(),
            //            Key = "Jurisdiction",
            //            Operator = Operators.Contains.ToString(),
            //            Value = jurisdiction
            //        }
            //};

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "userID",
                Operator = Operators.EqualTo.ToString(),
                Value = _user.UserId
            });

            var chats = _dbService.GetNSortedData(_chatCollectionName, filters, 20, "logDate", false);

            response = FormatChatHistory(chats);
            return response;
        }

        public List<UserChat> GetUserChatHistory(LPAUser user)
        {
            List<UserChat> response = new List<UserChat>();
            string jurisdiction = _user.Jurisdiction;

            List<FilterCriteria> filters = new List<FilterCriteria>();

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "userID",
                Operator = Operators.EqualTo.ToString(),
                Value = user.UserId
            });

            //filters.Add(new FilterCriteria()
            //{
            //    DataType = DataType.String.ToString(),
            //    Key = "isCleared",
            //    Operator = Operators.EqualTo.ToString(),
            //    Value = "false"
            //});

            var chats = _dbService.GetNSortedData(_chatCollectionName, filters, 20, "logDate", false);

            response = FormatChatHistory(chats);
            return response;
        }

        private void SaveChatIntent(Intent intent)
        {
            UserChat chat = new UserChat();

            string authToken = (string)_serviceRequestData.Get(LPAConsts.AuthToken);
            LPAUser user = _userManagement.GetUser(authToken);

            chat.intent = intent.topScoringIntent;
            chat.query = intent.query;
            chat.entities = intent.entities;
            chat.userID = user.UserId;
            chat.logDate = DateTime.Now;

            _dbService.Insert<UserChat>(_chatCollectionName, chat);
        }

        private void SaveChat(Intent intent, ChatResponse response)
        {
            UserChat chat = new UserChat();

            string authToken = (string)_serviceRequestData.Get(LPAConsts.AuthToken);
            LPAUser user = _userManagement.GetUser(authToken);

            chat.intent = intent.topScoringIntent;
            chat.query = intent.query;
            chat.entities = intent.entities;
            chat.userID = user.UserId;
            chat.response = response.ResponseMessage;
            chat.responseType = response.ContentType.ToString();
            chat.isCleared = false;
            chat.logDate = DateTime.Now;

            _dbService.Insert<UserChat>(_chatCollectionName, chat);
        }

        private List<string> FormatSuggestion(List<JObject> chats, Intent intent)
        {
            List<string> suggestions = new List<string>();
            foreach (var item in chats)
            {
                if (suggestions.Count >= _numberOfSuggestions)
                    break;

                var chatIntent = JsonConvert.DeserializeObject<ChildIntent>(item["intent"].ToString());

                switch (intent.topScoringIntent.intent)
                {
                    case "None":
                        if (chatIntent.intent != "None" && chatIntent.intent != "Conversation")
                            suggestions.Add(item["query"].ToString());
                        break;

                    default:
                        var chatEntities = JsonConvert.DeserializeObject<Entity[]>(item["entities"].ToString());
                        foreach (var entity in chatEntities)
                        {
                            if(entity.type == "GameName")
                            {
                                suggestions.Add(entity.entity);
                                continue;
                            }
                            suggestions.Add(item["query"].ToString());
                        }
                        break;
                }
            }

            suggestions = suggestions.Count > 0 ? suggestions.Distinct().ToList() : GetDefaultSuggestions(intent);

            return suggestions;
        }

        private List<UserChat> FormatChatHistory(List<JObject> chats)
        {
            List<UserChat> chatHistory = new List<UserChat>();

            for (int i = 0; i < chats.Count; i++)
            {
                if (chatHistory.Count > 25)
                    break;

                var item = chats[i];

                var chat = new UserChat();
                chat.query = item["query"].ToString();
                chat.response = item["response"].ToString();
                chat.responseType = item["responseType"].ToString();
                //chat.logDate = Convert.ToDateTime(item["logDate"].ToString());

                if (chat != null)
                    chatHistory.Insert(0,chat);
            }
           
            return chatHistory;
        }

        private List<string> GetDefaultSuggestions(Intent intent)
        {
            List<string> suggestions = new List<string>();

            if (intent.topScoringIntent.actions != null && intent.topScoringIntent.actions.Any())
            {
                string entityType = (from parameter in intent.topScoringIntent.actions[0].parameters
                                     where parameter.value == null
                                     select parameter.type).FirstOrDefault();

                switch (entityType)
                {
                    case "GameName":
                        suggestions = Enum.GetNames(typeof(GameNames)).ToList<string>();
                        break;
                    default:
                        break;
                }
            }

            return suggestions;
        }
    
    }
}