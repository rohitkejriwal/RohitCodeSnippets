using IOCInfrastructure;
using Sgi.LPA.Common.AI;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Chat;
using Sgi.LPA.Common.NLP;
using System;
using System.Collections.Generic;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.UAM;

namespace Sgi.LPA.AI
{
    /// <summary>
    /// test this test
    /// </summary>
    /// <seealso cref="Sgi.LPA.Common.AI.IAIEngine" />
    public class AIEngine : IAIEngine
    {
        private ILogHelper _logHelper;
        private IServiceResolver _serviceResolver;
        private IChatContext _chatContext;
        private IServiceTransactionData _serviceRequestData;
        private ILPAUAM _userManagement;

        public AIEngine(IServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
            _logHelper = _serviceResolver.GetInstance<ILogHelper>();
            _chatContext = _serviceResolver.GetInstance<IChatContext>();
            _serviceRequestData = _serviceResolver.GetInstance<IServiceTransactionData>();
            _userManagement = _serviceResolver.GetInstance<ILPAUAM>();
        }

        /// <summary>
        /// find most appropriate auto-correct sentence
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string JustifyAutoCorrect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save Chat in chat history data base
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveChat(Intent intent, IDataChannel dataChannel)
        {
            _logHelper.LogDebug("FindChatContextEntities", "Enter to Find Chat Context Entities", null, this);
            _chatContext.SaveChatHistory(intent);
            _logHelper.LogStep("Exit form find chat context entities", this);
        }

        /// <summary>
        /// Save Chat in chat history data base
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveChat(Intent intent, ChatResponse response)
        {
            _logHelper.LogDebug("FindChatContextEntities", "Enter to Find Chat Context Entities", null, this);
            _chatContext.SaveChatHistory(intent, response);
            _logHelper.LogStep("Exit form find chat context entities", this);
        }

        /// <summary>
        /// Find suggestions from Chat history.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<string> GetSuggestions(Intent intent)
        {
            _logHelper.LogDebug("FindChatContextEntities", "Enter to Find Chat Context Entities", null, this);
            List<string> suggestions = _chatContext.GetSuggestions(intent);
            _logHelper.LogStep("Exit form find chat context entities", this);
            return suggestions;
        }

        public List<ChatHistory> GetChatHistory()
        {
            List<UserChat> chatHistory = _chatContext.GetChatHistory();
            
            return GetChatHistoryMessagesFromChat(chatHistory);
        }

        public List<ChatHistory> GetUserChatHistory(LPAUser user)
        {
            List<UserChat> chatHistory = _chatContext.GetUserChatHistory(user);

            return GetChatHistoryMessagesFromChat(chatHistory);
        }

        /// <summary>
        /// Finds the chat channel.
        /// </summary>
        /// <param name="chatMessage">The chat message.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<Common.Channel.IDataChannel> FindChatChannel(Intent chatIntent)
        {
            _logHelper.LogDebug("FindChatChannel", "Enter to Find FindChatChannel", null, this);

            string authToken = _serviceRequestData.GetAuthToken();

            LPAUser user = _userManagement.GetUser(authToken);

            List<IDataChannel> dataChannels = new List<IDataChannel>();

            foreach (var channelName in user.DataChannels)
            {
                dataChannels.Add(_serviceResolver.GetInstance<IDataChannel>(channelName));
            }
            _logHelper.LogStep("Exit form find FindChatChannel", this);
            return dataChannels;
        }
        public Common.Channel.IDataChannel FindChatChannel(string channelName)
        {
            _logHelper.LogDebug( string.Format("Finding Data Channel : {0}", channelName), "Enter to Find FindChatChannel", null, this);
            var dataChannel = _serviceResolver.GetInstance<IDataChannel>(channelName);
            _logHelper.LogStep("Exit form find FindChatChannel", this);
            return dataChannel;
        }

        private List<ChatHistory> GetChatHistoryMessagesFromChat(List<UserChat> chats)
        {
            List<ChatHistory> chatHistoryMessages = new List<ChatHistory>();

            foreach(var item in chats)
            {
                chatHistoryMessages.Add(
                    new ChatHistory()
                    {
                        ResponseMessage = item.query,
                        ContentType = item.responseType,
                        messageType = "Query"

                    }
                    );

                chatHistoryMessages.Add(
                    new ChatHistory()
                    {
                        ResponseMessage = item.response,
                        ContentType = item.responseType,
                        messageType = "Response"

                    }
                    );
            }

            return chatHistoryMessages;
        }
    }


}