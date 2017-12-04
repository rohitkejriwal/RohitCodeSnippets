using System;
using System.Linq;

namespace Logger.Core
{
    public static class Utility
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        public static string GetRandomNumber(int length)
        {
            var chars = "1234567890";

            lock (syncLock)
            {
                var result = new string(
                    Enumerable.Repeat(chars, length)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                return result;
            }
        }

        public static long GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                var result = random.Next(min, max);
                return result;
            }
        }

        public static string GetRandomString(int length)
        {
            lock (syncLock)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

                var result = new string(
                    Enumerable.Repeat(chars, length)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());
                return result;
            }
        }

        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("ddMMyymmhhssfff");
        }
    }
}