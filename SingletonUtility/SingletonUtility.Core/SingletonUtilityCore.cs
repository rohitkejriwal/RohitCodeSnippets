using System;
using System.Reflection;

namespace SingletonUtility.Core
{
    public sealed class SingletonUtility<T> where T : class
    {
        #region Variable
        private volatile static T singleton;
        private static object syncRoot = new Object();
        #endregion

        #region Private Constructor
        private SingletonUtility()
        {
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Returns Singleton Instance
        /// </summary>
        public static T Instance
        {
            get
            {
                if (singleton == null)
                {
                    lock (syncRoot)
                    {
                        if (singleton == null)
                        {
                            singleton = ReflectionConstructor();
                        }
                    }
                }
                return singleton;
            }
        }

        /// <summary>
        /// Reset Singleton Instance
        /// </summary>
        public static void Reset()
        {
            singleton = default(T);
        }
        #endregion


        #region Private Method
        private static T ReflectionConstructor()
        {
            ConstructorInfo ci = typeof(T).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.CreateInstance, null, new Type[0], null);
            return (T)ci.Invoke(null);
        }
        #endregion
    }
}
