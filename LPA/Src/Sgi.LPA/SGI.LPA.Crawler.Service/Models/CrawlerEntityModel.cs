﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.Crawler.Service.Models
{
    class CrawlerEntityModel
    {
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public int Index { get; set; }
    }
}