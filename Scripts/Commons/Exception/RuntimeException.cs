using System;

namespace Commons.Exceptions
{
    public class RuntimeException : Exception
    {

        public RuntimeException()
        {

        }

        public RuntimeException(string message)
            : base(message)
        {
        }

        public RuntimeException(System.Object o)
            : base(o == null ? null : o.ToString())
        {

        }
    }
}