using System;

namespace Commons.Debugging.Logging
{
    public class SimpleGuiLogger: IGuiLogger
    {
        private readonly Action _callback;

        public SimpleGuiLogger(Action callback)
        {
            _callback = callback;
        }

        public void Log()
        {
            _callback();
        }
    }
}