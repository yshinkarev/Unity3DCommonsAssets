using UnityEngine;

namespace Commons.Utils.Pooling
{
    [System.Serializable]
    public class StartupPool
    {
        public int Size = 0;
        public GameObject Prefab = null;

        [HideInInspector]
        public int MaxSpawned;
    }
}