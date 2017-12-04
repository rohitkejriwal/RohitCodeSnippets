using System.Collections.Generic;

namespace Logger.Domain
{
    public static class LogLevels
    {
        public const int Off = 0;
        public const int Debug = 1;
        public const int Info = 2;
        public const int Error = 3;
        public const int Enter = 10;
        public const int Exit = 11;
        public const int Timespan = 20;

        public static List<int> DefaultLevels = new List<int>() { 0, 1, 2, 3, 10, 11, 20 };
    }
}