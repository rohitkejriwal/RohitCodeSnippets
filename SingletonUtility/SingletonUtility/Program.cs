using SingletonUtility.Engine;
using SingletonUtility.Core;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var treatmentEngine = SingletonUtility<TreatmentEngine>.Instance;
            var messageEngine = SingletonUtility<MessageEngine>.Instance;

            var treatmentEngine1 = SingletonUtility<TreatmentEngine>.Instance;
            var messageEngine1 = SingletonUtility<MessageEngine>.Instance;

            treatmentEngine.TestEngine();
            messageEngine.TestEngine();
        }
    }
}
