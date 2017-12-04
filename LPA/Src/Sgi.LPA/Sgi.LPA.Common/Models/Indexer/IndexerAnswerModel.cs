using Sgi.LPA.Common.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sgi.LPA.Common.Models.Indexer
{
    public class IndexerAnswerModel
    {
        public int Index { get; set; }
        public string Device { get; set; }
        public ContentTypes ContentType { get; set; }
        public string Answer { get; set; }
    }
}
