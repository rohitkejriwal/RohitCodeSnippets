using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.LPA.Common.Models.Http
{
    public class MVCResponse
    {
        public int status { get; set; }
        public string Message { get; set; }
        public string Response { get; set; }
    }
}
