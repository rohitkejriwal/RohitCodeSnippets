using IOCInfrastructure;
using Newtonsoft.Json;
using Sgi.Core.DBService;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sgi.LPA.Common.Models.Indexer;
using System.Net;
using System.IO;
using Sgi.LPA.Common.Models.Http;
using Sgi.LPA.Common.Models.Enums;
using System.Configuration;
using Sgi.LPA.Common.Helper;
using Sgi.LPA.Common.Utilities;
using Sgi.LPA.Common.UAM;

namespace Sgi.LAP.IndexDBDataChannel
{
    public class IndexDBSearch : IDataChannel
    {
        private IDBRepository _dbService;
        private ILogHelper _logHelper;
        private const string _indexerCollectionName = "dataindex";        
        private readonly string webCrawlerUrl ;
        private readonly IServiceResolver _serviceResolver;
        private readonly Sgi.LPA.Common.Models.PAM.LPAUser _user = null;
        private readonly ILPAUAM _userManagement;

        public IndexDBSearch(IServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
            _dbService = serviceResolver.GetInstance<IDBRepository>(LPAConsts.MongoDBIndexRepositoryKey);
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
            _userManagement = serviceResolver.GetInstance<ILPAUAM>();
            string authToken = serviceResolver.GetInstance<IServiceTransactionData>().GetAuthToken();
            _user = _userManagement.GetUser(authToken);
            
        }

        public SearchQueryResonse SendQuery(LPA.Common.NLP.Intent fillIntent)
        {
            _logHelper.LogStep("Getting response from Index Db", this);
            string jurisdiction = _user.Jurisdiction;
            SearchQueryResonse queryResponse = new SearchQueryResonse();

            List<FilterCriteria> filters = new List<FilterCriteria>() 
            { 
                new FilterCriteria() 
                { 
                    DataType = DataType.String.ToString(), 
                    Key = "Intent", 
                    Operator = Operators.EqualTo.ToString(), 
                    Value = fillIntent.topScoringIntent.intent
                }
            };

            if(!string.IsNullOrEmpty(jurisdiction))
            {
                filters.Add(
                    new FilterCriteria()
                    {
                        DataType = DataType.StringList.ToString(),
                        Key = "Jurisdiction",
                        Operator = Operators.Contains.ToString(),
                        Value = jurisdiction
                    }
                    );
            }

            if (fillIntent.entities!=null && fillIntent.entities.Length > 0 && !string.IsNullOrEmpty(fillIntent.entities[0].type))
            {
                filters.Add(
                    new FilterCriteria()
                    {
                        DataType = DataType.EntityValues.ToString(),
                        Key = "Entities.Values",
                        Operator = Operators.Contains.ToString(),
                        Value = fillIntent.entities[0].entity
                    });
            }

            try
            {
                var indexerJObjects = _dbService.GetData(_indexerCollectionName, filters);
                _logHelper.LogStep("Response received from Index Db", this);
                var indexers = (from item in indexerJObjects
                                select JsonConvert.DeserializeObject<IndexerModel>(item.ToString())).ToList();

                queryResponse = PopulateDataFromDataSources(indexers);
                
            }
            catch(Exception ex )
            {
                _logHelper.LogError(ex, "IndexDBSearch", "SendQuery", null, this);
            }

            return queryResponse;
        }

        private SearchQueryResonse PopulateDataFromDataSources(List<IndexerModel> indexers)
        {
            SearchQueryResonse queryResponse = new SearchQueryResonse();

            foreach (var indexer in indexers)
            {
                while (queryResponse.Status == -1)
                {
                    var dbdataCrawlerSource = _serviceResolver.GetInstance<IDataCrawlerSource>(indexer.DataSource);
                    if (dbdataCrawlerSource != null)
                    {
                        queryResponse = dbdataCrawlerSource.GetCrawlerData(indexer);
                    }
                    else
                    {
                        _logHelper.LogError(new Exception("No IDataCrawlerSource found for source : " + indexer.DataSource), "IndexDBSearch", "PopulateDataFromDataSources",null, this);
                    }
                }
            }
            return queryResponse;
        }      
    }
}
