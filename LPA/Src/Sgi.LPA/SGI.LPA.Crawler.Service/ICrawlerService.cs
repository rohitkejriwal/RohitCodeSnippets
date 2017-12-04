using SGI.LPA.Crawler.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.Crawler.Service
{
    public interface ICrawlerService
    {
        string GetImageBytesFromWebsite(IndexerModel indexer);

        string GetStringValueFromWebsite(IndexerModel indexer);
    }
}
