using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Indexer.Service.Model
{
    public class IndexerModel
    {
        public string _id { get; set; }
        public string DefaultQuery { get; set; }
        public List<string> Jurisdiction { get; set; }
        public string DataSource { get; set; }
        public string Intent { get; set; }
        public ContentTypes ContentType { get; set; }
        public List<IndexerEntityModel> Entities { get; set; }
        public List<IndexerAnswerModel> Answers { get; set; }
    }
}
