using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sgi.Core.DBService
{
    public class FilterCriteria
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Operator { get; set; }
        public string DataType { get; set; }
    }

    public enum Operators
    {
        GreaterThanEqualTo,
        LessThanEqualTo,
        EqualTo,
        Contains
    }

    public enum DataType
    {
        String,
        DateTime,
        Int32,
        Int64,
        NumericList,
        Double,
        StringList,
        EntityValues,
        EntityType
    }
}
