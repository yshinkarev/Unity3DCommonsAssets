using System.Collections.Generic;
using Commons.Exceptions;

namespace Commons.AppEvents
{
    public class AppEventList
    {
        private static AppEventList _instance;

        // Now change listeners from set to list and on Get event fire to listeners to back order.
        // Because in some cases, I Get unread/unseen data to last opened screen, at this screen clear mark and want, that other listeners Get already marked data.
        private readonly List<IOnAppEvent> _listeners = new List<IOnAppEvent>();

        private readonly System.Object _lockThis = new System.Object();

        public static AppEventList Get()
        {
            return _instance ?? (_instance = new AppEventList());
        }

        private AppEventList()
        {
            //            Tracks.Me(Tracks.EVENT, "AppEventList created: {0}", this);
        }

        public void AddListener(IOnAppEvent onAppEventListener)
        {
            if (onAppEventListener != null)
                OnEventOperation(1, onAppEventListener);
        }

        public void RemoveListener(IOnAppEvent onAppEventListener)
        {
            if (onAppEventListener != null)
                OnEventOperation(0, onAppEventListener);
        }

        public void Fire(AppEvent appEvent)
        {
            if (appEvent != null)
                OnEventOperation(2, appEvent);
        }

        private void OnEventOperation(int mode, System.Object data)
        {
            lock (_lockThis)
            {
                if (mode < 2)
                {
                    IOnAppEvent listener = (IOnAppEvent)data;

                    if (mode == 0)
                        _listeners.Remove(listener);
                    else
                    {
                        if (!_listeners.Contains(listener))
                            _listeners.Add(listener);
                    }
                }
                else if (mode == 2)
                {
                    AppEvent appEvent = (AppEvent)data;

                    for (int i = _listeners.Count - 1; i > -1; i--)
                    {

                        IOnAppEvent listener = _listeners[i];
                        listener.OnEvent(appEvent);
                    }
                }
                else
                    throw new RuntimeException(mode);
            }
        }
    }
}