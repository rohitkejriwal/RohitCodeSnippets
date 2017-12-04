using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGI.LPA.LuisTrainer.Models
{
    public class EntityLabel
    {
        public string EntityType { get; set; }
        public int StartToken { get; set; }
        public int EndToken { get; set; }
    }

    public class UtteranceLabel
    {
        public string ExampleText { get; set; }
        public string SelectedIntentName { get; set; }
        public List<EntityLabel> EntityLabels { get; set; }
    }
}
