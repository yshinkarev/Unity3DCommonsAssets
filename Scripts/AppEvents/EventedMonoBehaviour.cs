using UnityEngine;

namespace Commons.AppEvents
{
    public class EventedMonoBehaviour : MonoBehaviour, IOnAppEvent
    {
        public virtual void Awake()
        {
            AppEventList.Get().AddListener(this);
        }

        public void OnDestroy()
        {
            AppEventList.Get().RemoveListener(this);
        }

        public virtual void OnEvent(AppEvent appEvent)
        {
        }
    }
}