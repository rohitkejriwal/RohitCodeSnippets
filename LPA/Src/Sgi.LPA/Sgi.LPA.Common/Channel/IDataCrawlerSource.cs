using Sgi.LPA.Common.Models.Chat;
using Sgi.LPA.Common.Models.Indexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Common.Channel
{
    public interface IDataCrawlerSource
    {
        SearchQueryResonse GetCrawlerData(IndexerModel indexDbModel);
    }
}
