using UnityEngine;
using System.Collections.Generic;
using Commons.Debugging.Diagnostic;

namespace Commons.Utils.Pooling
{
    public sealed class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        private static readonly List<GameObject> _tempList = new List<GameObject>();

        private readonly Dictionary<GameObject, List<GameObject>> _pooledObjects = new Dictionary<GameObject, List<GameObject>>();

        private readonly Dictionary<GameObject, GameObject> _spawnedObjects = new Dictionary<GameObject, GameObject>();

        public StartupPoolMode StartupPoolMode = StartupPoolMode.Awake;
        public StartupPool[] StartupPools = null;

        private bool _startupPoolsCreated;

        private void Awake()
        {
            _instance = this;

            if (StartupPoolMode == StartupPoolMode.Awake)
                CreateStartupPools();
        }

        private void Start()
        {
            if (StartupPoolMode == StartupPoolMode.Start)
                CreateStartupPools();
        }

        ///////////////////////////////////////////////// Initialize /////////////////////////////////////////////////

        public static void CreateStartupPools()
        {
            if (Instance._startupPoolsCreated)
                return;

            Instance._startupPoolsCreated = true;

            StartupPool[] pools = Instance.StartupPools;

            if (pools == null || pools.Length <= 0)
                return;

            foreach (StartupPool t in pools)
                CreatePool(t.Prefab, t.Size);
        }

        public static void CreatePool<T>(T prefab, int initialPoolSize) where T : Component
        {
            CreatePool(prefab.gameObject, initialPoolSize);
        }

        public static void CreatePool(GameObject prefab, int initialPoolSize)
        {
            if (prefab == null || Instance._pooledObjects.ContainsKey(prefab))
                return;

            List<GameObject> list = new List<GameObject>();
            Instance._pooledObjects.Add(prefab, list);

            if (initialPoolSize <= 0)
                return;

            bool active = prefab.activeSelf;
            prefab.SetActive(false);
            Transform parent = Instance.transform;

            while (list.Count < initialPoolSize)
            {
                GameObject obj = Instantiate(prefab);
                obj.transform.parent = parent;
                list.Add(obj);
            }

            prefab.SetActive(active);
        }

        ///////////////////////////////////////////////// Spawn /////////////////////////////////////////////////

        public static T Spawn<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation) where T : Component
        {
            return Spawn(prefab.gameObject, parent, position, rotation).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            return Spawn(prefab.gameObject, null, position, rotation).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab, Transform parent, Vector3 position) where T : Component
        {
            return Spawn(prefab.gameObject, parent, true, position, false, MyQuaternion.Identity).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab, Vector3 position) where T : Component
        {
            return Spawn(prefab.gameObject, null, true, position, false, MyQuaternion.Identity).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab, Transform parent) where T : Component
        {
            return Spawn(prefab.gameObject, parent, false, MyVector.Zero, false, MyQuaternion.Identity).GetComponent<T>();
        }

        public static T Spawn<T>(T prefab) where T : Component
        {
            return Spawn(prefab.gameObject, null, false, MyVector.Zero, false, MyQuaternion.Identity).GetComponent<T>();
        }

        public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefab, parent, true, position, true, rotation);
        }

        public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position)
        {
            return Spawn(prefab, parent, true, position, false, MyQuaternion.Identity);
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefab, null, position, rotation);
        }

        public static GameObject Spawn(GameObject prefab, Transform parent)
        {
            return Spawn(prefab, parent, false, MyVector.Zero, false, MyQuaternion.Identity);
        }

        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            return Spawn(prefab, null, true, position, false, MyQuaternion.Identity);
        }

        public static GameObject Spawn(GameObject prefab)
        {
            return Spawn(prefab, null, false, MyVector.Zero, false, MyQuaternion.Identity);
        }

        ///////////////////////////////////////////////// Recycle /////////////////////////////////////////////////

        public static void Recycle<T>(T obj) where T : Component
        {
            Recycle(obj.gameObject);
        }

        public static void Recycle(GameObject obj)
        {
            GameObject prefab;

            if (Instance._spawnedObjects.TryGetValue(obj, out prefab))
                Recycle(obj, prefab);
            else
            {
                Track.Me(Track.POOL, "Hm! Destroy: {0}", obj);
                Destroy(obj);
            }
        }

        private static void Recycle(GameObject obj, GameObject prefab)
        {
            Instance._pooledObjects[prefab].Add(obj);
            Instance._spawnedObjects.Remove(obj);
            obj.transform.parent = Instance.transform;
            obj.SetActive(false);
            UpdateSpawnStat(prefab, -1);
            Track.Me(Track.POOL, "Recycle: {0}", obj);
        }

        public static void RecycleAll<T>(T prefab) where T : Component
        {
            RecycleAll(prefab.gameObject);
        }

        public static void RecycleAll(GameObject prefab)
        {
            foreach (KeyValuePair<GameObject, GameObject> item in Instance._spawnedObjects)
                if (item.Value == prefab)
                    _tempList.Add(item.Key);

            foreach (GameObject t in _tempList)
                Recycle(t);

            _tempList.Clear();
        }

        public static int RecycleAll()
        {
            _tempList.AddRange(Instance._spawnedObjects.Keys);
            int count = 0;

            foreach (GameObject t in _tempList)
            {
                Recycle(t);
                count++;
            }

            _tempList.Clear();
            return count;
        }

        ///////////////////////////////////////////////// Get... /////////////////////////////////////////////////

        public static bool IsSpawned(GameObject obj)
        {
            return Instance._spawnedObjects.ContainsKey(obj);
        }

        public static int CountPooled<T>(T prefab) where T : Component
        {
            return CountPooled(prefab.gameObject);
        }

        public static int CountPooled(GameObject prefab)
        {
            List<GameObject> list;
            return Instance._pooledObjects.TryGetValue(prefab, out list) ? list.Count : 0;
        }

        public static int CountSpawned<T>(T prefab) where T : Component
        {
            return CountSpawned(prefab.gameObject);
        }

        public static int CountSpawned(GameObject prefab)
        {
            int count = 0;

            foreach (GameObject instancePrefab in Instance._spawnedObjects.Values)
                if (prefab == instancePrefab)
                    ++count;

            return count;
        }

        public static int CountAllPooled()
        {
            int count = 0;

            foreach (List<GameObject> list in Instance._pooledObjects.Values)
                count += list.Count;

            return count;
        }

        public static List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
        {
            if (list == null)
                list = new List<GameObject>();

            if (!appendList)
                list.Clear();

            List<GameObject> pooled;

            if (Instance._pooledObjects.TryGetValue(prefab, out pooled))
                list.AddRange(pooled);

            return list;
        }

        public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
        {
            if (list == null)
                list = new List<T>();

            if (!appendList)
                list.Clear();

            List<GameObject> pooled;

            if (!Instance._pooledObjects.TryGetValue(prefab.gameObject, out pooled))
                return list;

            for (int i = 0; i < pooled.Count; ++i)
                list.Add(pooled[i].GetComponent<T>());

            return list;
        }

        public static List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
        {
            if (list == null)
                list = new List<GameObject>();

            if (!appendList)
                list.Clear();

            foreach (KeyValuePair<GameObject, GameObject> item in Instance._spawnedObjects)
                if (item.Value == prefab)
                    list.Add(item.Key);

            return list;
        }

        public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
        {
            if (list == null)
                list = new List<T>();

            if (!appendList)
                list.Clear();

            GameObject prefabObj = prefab.gameObject;

            foreach (KeyValuePair<GameObject, GameObject> item in Instance._spawnedObjects)
                if (item.Value == prefabObj)
                    list.Add(item.Key.GetComponent<T>());

            return list;
        }

        public static List<GameObject> GetAllSpawned()
        {
            List<GameObject> result = new List<GameObject>();
            StartupPool[] pools = Instance.StartupPools;

            foreach (StartupPool sp in pools)
                GetSpawned(sp.Prefab, result, true);

            return result;
        }

        ///////////////////////////////////////////////// Destroy /////////////////////////////////////////////////

        public static void DestroyPooled(GameObject prefab)
        {
            List<GameObject> pooled;

            if (!Instance._pooledObjects.TryGetValue(prefab, out pooled))
                return;

            foreach (GameObject t in pooled)
                Destroy(t);

            pooled.Clear();
        }

        public static void DestroyPooled<T>(T prefab) where T : Component
        {
            DestroyPooled(prefab.gameObject);
        }

        public static void DestroyAll(GameObject prefab)
        {
            RecycleAll(prefab);
            DestroyPooled(prefab);
        }

        public static void DestroyAll<T>(T prefab) where T : Component
        {
            DestroyAll(prefab.gameObject);
        }

        ///////////////////////////////////////////////////////////////////

        public static ObjectPool Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<ObjectPool>();

                if (_instance != null)
                    return _instance;

                GameObject obj = new GameObject("ObjectPool");
                obj.transform.localPosition = MyVector.Zero;
                obj.transform.localRotation = MyQuaternion.Identity;
                obj.transform.localScale = MyVector.One;
                _instance = obj.AddComponent<ObjectPool>();

                return _instance;
            }
        }

        // Private.

        private static GameObject Spawn(GameObject prefab, Transform parent, bool setPosition, Vector3 position, bool setRotation, Quaternion rotation)
        {
            List<GameObject> list;
            Transform trans;
            GameObject obj;

            if (Instance._pooledObjects.TryGetValue(prefab, out list))
            {
                obj = null;

                if (list.Count > 0)
                {
                    while (obj == null && list.Count > 0)
                    {
                        obj = list[0];
                        list.RemoveAt(0);
                    }

                    if (obj != null)
                    {
                        trans = obj.transform;
                        trans.parent = parent;
                        SetPositionAndRotation(trans, setPosition, position, setRotation, rotation);
                        obj.SetActive(true);
                        Instance._spawnedObjects.Add(obj, prefab);
                        UpdateSpawnStat(prefab, 1);
                        Track.Me(Track.POOL, "Spawn (from pool): {0}", obj);
                        return obj;
                    }
                }

                obj = Instantiate(prefab);
                trans = obj.transform;
                trans.parent = parent;
                SetPositionAndRotation(trans, setPosition, position, setRotation, rotation);
                Instance._spawnedObjects.Add(obj, prefab);
                UpdateSpawnStat(prefab, 1);
                Track.Me(Track.POOL, "Spawn (from new): {0}", obj);
                return obj;
            }

            obj = Instantiate(prefab);
            trans = obj.GetComponent<Transform>();
            trans.parent = parent;
            SetPositionAndRotation(trans, setPosition, position, setRotation, rotation);
            Track.Me(Track.POOL, "Initialize new: {0}", obj);
            return obj;
        }

        private static void SetPositionAndRotation(Transform trans, bool setPosition, Vector3 position, bool setRotation,
            Quaternion rotation)
        {
            if (setPosition)
                trans.localPosition = position;

            if (setRotation)
                trans.localRotation = rotation;
        }

        private static void UpdateSpawnStat(GameObject prefab, int change)
        {
            foreach (StartupPool sp in Instance.StartupPools)
                if (sp.Prefab == prefab)
                    sp.MaxSpawned += change;
        }
    }
}