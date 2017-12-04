using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.Crawler.Service.Models
{
    public class IndexerAnswerModel
    {
        public int Index { get; set; }
        public string Device { get; set; }
        public ContentTypes ContentType { get; set; }
        public string Answer { get; set; }
    }

    public enum ContentTypes
    {
        text,
        image,
        date,
        hyperlink,
        html
    }
}
