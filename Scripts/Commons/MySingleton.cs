using System;

namespace Commons
{
    public abstract class MySingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (T)Activator.CreateInstance(typeof(T));

                return _instance;
            }
        }

        public static void ForgetInstance()
        {
            _instance = default(T);
        }

        protected MySingleton()
        {

        }
    }
}