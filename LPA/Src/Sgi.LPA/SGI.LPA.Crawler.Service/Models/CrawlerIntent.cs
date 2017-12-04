using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.Crawler.Service.Models
{
    class CrawlerIntent
    {
        public string _id { get; set; }
        public string DefaultQuery { get; set; }
        public List<string> Jurisdiction { get; set; }
        public string DataSource { get; set; }
        public string Intent { get; set; }
        public List<CrawlerEntityModel> Entities { get; set; }
        //public List<IndexerAnswerModel> Answers { get; set; }
        public string ContentType { get; set; }
        public string Url { get; set;}
        public string Selector { get; set; }
    }
}
