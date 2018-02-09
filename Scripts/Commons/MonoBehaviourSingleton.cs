using UnityEngine;

namespace Commons
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour 
    {
        public static T Instance;

        protected virtual void Awake()
        {
            Instance = GetComponent<T>();
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }
    }
}