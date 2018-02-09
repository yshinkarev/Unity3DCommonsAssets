using System.Collections.Generic;
using Commons.Utils;

namespace Commons.Debugging.Logging
{
    public class GuiLoggerList
    {
        private readonly object _lock;
        private readonly List<IGuiLogger> _guiLoggers;
        private readonly Dictionary<object, IGuiLogger> _connections;

        public GuiLoggerList()
        {
            _lock = new object();
            _guiLoggers = new List<IGuiLogger>();
            _connections = new Dictionary<object, IGuiLogger>();
        }

        //        lock (_lock)

        public void AddGuiLogger(IGuiLogger logger, object source = null)
        {
            lock (_lock)
            {
                if (_guiLoggers.Contains(logger))
                    return;

                _guiLoggers.Add(logger);

                if (source != null)
                    _connections[source] = logger;
            }
        }

        public void RemoveLogger(object source)
        {
            lock (_lock)
            {
                IGuiLogger listener = U.GetValueOrThrow(_connections, source);
                _connections.Remove(listener);
                _guiLoggers.Remove(listener);
            }
        }

        public void RemoveLogger(IGuiLogger logger)
        {
            lock (_lock)
            {
                _guiLoggers.Remove(logger);

            }
        }

        public void OnGui()
        {
            lock (_lock)
            {
                foreach (IGuiLogger logger in _guiLoggers)
                    logger.Log();
            }
        }
    }
}
