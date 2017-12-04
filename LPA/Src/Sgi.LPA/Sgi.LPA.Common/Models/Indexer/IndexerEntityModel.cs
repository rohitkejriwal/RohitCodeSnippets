using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sgi.LPA.Common.Models.Indexer
{
    public class IndexerEntityModel
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public int Index { get; set; }
    }
}
