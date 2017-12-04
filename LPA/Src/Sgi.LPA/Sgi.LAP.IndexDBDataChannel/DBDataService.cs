using IOCInfrastructure;
using Sgi.LPA.Common.Channel;
using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.Enums;
using Sgi.LPA.Common.Models.Indexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LAP.IndexDBDataChannel
{
    public class DBDataService : IDataCrawlerSource
    {

        private ILogHelper _logHelper;

        public DBDataService(IServiceResolver serviceResolver)
        {
            _logHelper = serviceResolver.GetInstance<ILogHelper>();
        }
        public SearchQueryResonse GetCrawlerData(IndexerModel indexDbModel)
        {
            _logHelper.LogStep("Enter Get data from DBDataService.DBDataService");
            SearchQueryResonse queryResponse = new SearchQueryResonse();

            var answer = (from ans in indexDbModel.Answers
                          where ans.Index == 0
                          select ans).FirstOrDefault();

            switch (answer.ContentType)
            {
                case ContentTypes.date:
                    queryResponse.Responses = new List<string>() { GenerateMockDate().ToShortDateString() };
                    queryResponse.Status = 0;
                    break;
                case ContentTypes.html:
                    queryResponse.Responses = new List<string>() { answer.Answer };
                    queryResponse.Status = 0;
                    break;
                case ContentTypes.hyperlink:
                    queryResponse.Responses = new List<string>() { answer.Answer };
                    queryResponse.Status = 0;
                    break;
                case ContentTypes.image:
                    queryResponse.Responses = new List<string>() { answer.Answer };
                    queryResponse.Status = 0;
                    break;
                default:
                    queryResponse.Responses = new List<string>() { answer.Answer };
                    queryResponse.Status = 0;
                    break;
            }

            _logHelper.LogStep("Exit Get data from DBDataService.DBDataService");
            return queryResponse;
        }
        private DateTime GenerateMockDate()
        {
            Random rnd = new Random();
            int days = rnd.Next(1, 7);

            DateTime date = DateTime.Now.AddDays(days);

            return date;
        }
    }
}
