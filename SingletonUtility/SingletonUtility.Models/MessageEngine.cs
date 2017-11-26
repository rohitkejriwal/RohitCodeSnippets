using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonUtility.Engine
{
    public class MessageEngine
    {
        private MessageEngine()
        { }

        public void TestEngine()
        {
            Console.WriteLine("This is Message Engine");
        }
    }
}
