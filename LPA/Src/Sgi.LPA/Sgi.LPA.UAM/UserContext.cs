using IOCInfrastructure;
using Newtonsoft.Json;
using Sgi.Core.DBService;
using Sgi.LPA.Common.Models.PAM;
using Sgi.LPA.Common.UAM;
using Sgi.LPA.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.UAM
{
    public class UserContext : IUserContext
    {
        private IDBRepository _dbService;
        private const string _userCollectionName = "user";

        public UserContext(IServiceResolver serviceResolver)
        {
            _dbService = serviceResolver.GetInstance<IDBRepository>(LPAConsts.MongoDBUserRepositoryKey);
        }

        public Common.Models.PAM.LPAUser GetUser(string authToken)
        {
            List<string> response = new List<string>();

            List<FilterCriteria> filters = new List<FilterCriteria>();

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "AuthToken",
                Operator = Operators.EqualTo.ToString(),
                Value = authToken
            });

            try
            {
                var userData = _dbService.GetData(_userCollectionName, filters);

                if (userData.Count <= 0)
                    return null;

                var user = JsonConvert.DeserializeObject<LPAUser>(userData.First().ToString());

                return user;
            }
            catch
            {
                return null;
            }
        }

        public string GetUserAuthToken(string userName, string phone)
        {
            LPAUser user = GetLPAUser(userName, phone);

            if(user == null)
                user = CreateUser(userName, phone);

            if (user != null)
                return user.AuthToken;

            return user.AuthToken;
            
        }

        public LPAUser CreateUser(string userName, string phone)
        {
            LPAUser user = new LPAUser();

            user.userName = userName;
            user.phone = phone;
            user.UserId = userName;
            user.Password = userName;
            user.Jurisdiction = "ALC";
            user.DataChannels = new System.Collections.Generic.List<string>() { LPAConsts.DataChannelCommonKey, LPAConsts.DataChannelIndexDBSearchKey };
            user.AuthToken = Guid.NewGuid().ToString();

            _dbService.Insert<LPAUser>(_userCollectionName, user);

            return user;
        }
    
        public LPAUser GetLPAUser(string userName, string phone)
        {
            List<string> response = new List<string>();

            List<FilterCriteria> filters = new List<FilterCriteria>();

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "userName",
                Operator = Operators.EqualTo.ToString(),
                Value = userName
            });

            filters.Add(new FilterCriteria()
            {
                DataType = DataType.String.ToString(),
                Key = "phone",
                Operator = Operators.EqualTo.ToString(),
                Value = phone
            });

            try
            {
                var userData = _dbService.GetData(_userCollectionName, filters);

                if (userData.Count <= 0)
                    return null;

                var user = JsonConvert.DeserializeObject<LPAUser>(userData.First().ToString());

                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
