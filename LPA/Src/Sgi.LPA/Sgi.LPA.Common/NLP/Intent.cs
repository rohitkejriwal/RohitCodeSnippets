namespace Sgi.LPA.Common.NLP
{
    public class Intent
    {
        public string _id { get; set; }
        public string query { get; set; }
        public ChildIntent topScoringIntent { get; set; }
        public ChildIntent[] intents { get; set; }
        public Entity[] entities { get; set; }
        public Dialog dialog { get; set; }
        
    }

    public class ChildIntent
    {
        public string intent { get; set; }
        public float score { get; set; }
        public Action[] actions { get; set; }
    }

    public class Action
    {
        public bool triggered { get; set; }
        public string name { get; set; }
        public Parameter[] parameters { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public object value { get; set; }
    }

    public class Dialog
    {
        public string prompt { get; set; }
        public string parameterName { get; set; }
        public string parameterType { get; set; }
        public string contextId { get; set; }
        public string status { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }
}
