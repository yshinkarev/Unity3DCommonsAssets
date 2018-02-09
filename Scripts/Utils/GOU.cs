using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Commons.Utils
{
    // ReSharper disable once InconsistentNaming
    public static class GOU
    {
        private delegate Boolean IsTargetObjectDelegate(GameObject go, string pattern);

        public static GameObject FindInactive(string name, bool isRooted = true)
        {
            return FindInactiveInternal(name, IsTargetObjectByName, isRooted);
        }

        public static GameObject FindInactiveByTag(string tag, bool isRooted = true)
        {
            return FindInactiveInternal(tag, IsTargetObjectByTag, isRooted);
        }

        public static GameObject[] FindAllDirectChildsArray(GameObject parent)
        {
            return FindAllDirectChilds(parent).ToArray();
        }

        public static List<GameObject> FindAllDirectChildsList(GameObject parent)
        {
            return FindAllDirectChilds(parent).ToList();
        }

        public static IEnumerable<GameObject> FindAllDirectChilds(GameObject parent)
        {
            Transform tParent = parent.transform;
            return parent.GetComponentsInChildren<Transform>(true).Where(t => t.parent == tParent).Select(t => t.gameObject);
        }

        public static GameObject FindDirectChild(string name, GameObject parent)
        {
            Transform tParent = parent.transform;
            return parent.GetComponentsInChildren<Transform>(true).Where(t => t.parent == tParent && t.name == name).Select(t => t.gameObject).FirstOrDefault();
        }

        public static GameObject FindChild(string name, GameObject parent)
        {
            return parent.GetComponentsInChildren<Transform>(true).Where(t => t.name == name).Select(t => t.gameObject).FirstOrDefault();
        }


        public static T FindComponentOfInactiveChild<T>(string name, GameObject parent) where T : Component
        {
            GameObject go = FindInactive(name, parent);
            return go == null ? null : FindComponentOfChild<T>(go);
        }

        public static T FindComponentOfChild<T>(GameObject parent) where T : Component
        {
            return parent.GetComponentsInChildren<Transform>(true).Where(t => t.gameObject.GetComponent<T>() != null).Select(t => t.gameObject.GetComponent<T>()).FirstOrDefault();
        }

        public static T FindComponentOfChild<T>(string name, GameObject parent) where T : Component
        {
            return parent.GetComponentsInChildren<Transform>(true).Where(t => t.name == name).Select(t => t.gameObject.GetComponent<T>()).FirstOrDefault();
        }

        public static GameObject FindInactive(string name, GameObject parent)
        {
            return parent.transform.GetComponentsInChildren<Transform>(true).First(c => c.name == name).gameObject;
        }

        public static GameObject FindInactiveByTag(string tag, GameObject parent)
        {
            return parent.transform.GetComponentsInChildren<Transform>(true).First(c => c.CompareTag(tag)).gameObject;
        }

        public static void SetActive(bool active, params MonoBehaviour[] objects)
        {
            if (objects == null)
                return;

            foreach (MonoBehaviour mb in objects)
                mb.enabled = active;
        }

        public static void SetActive(bool active, params GameObject[] objects)
        {
            if (objects == null)
                return;

            foreach (GameObject go in objects)
                go.SetActive(active);
        }

        public static void SetActive(bool active, IEnumerable<GameObject> objects)
        {
            if (objects == null)
                return;

            foreach (GameObject go in objects)
                go.SetActive(active);
        }

        public static void Destroy(IEnumerable<GameObject> objects)
        {
            if (objects == null)
                return;

            foreach (GameObject go in objects)
                UnityEngine.Object.Destroy(go);
        }

        public static GameObject GetGameObject<T>(T o)
        {
            if (o is GameObject)
                return o as GameObject;

            if (o is Component)
                return (o as Component).gameObject;

            throw new Exception(o.GetType().ToString());
        }

        // Private.

        private static GameObject FindInactiveInternal(string pattern, IsTargetObjectDelegate targetDelegate, bool isRooted)
        {
            GameObject[] all = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject o in all)
            {
                if (isRooted && o.transform.parent != null)
                    continue;

                if (o.hideFlags == HideFlags.NotEditable || o.hideFlags == HideFlags.HideAndDontSave)
                    continue;

#if UNITY_EDITOR
                if (Application.isEditor)
                {
                    string sAssetPath = UnityEditor.AssetDatabase.GetAssetPath(o.transform.root.gameObject);

                    if (!string.IsNullOrEmpty(sAssetPath))
                        continue;
                }
#endif

                if (targetDelegate(o, pattern))
                    return o;

                GameObject go = FindInactiveInternal(o, pattern);

                if (go != null)
                    return go;
            }

            throw new Exception("Not found: " + pattern);
        }

        private static GameObject FindInactiveInternal(GameObject go, string name)
        {
            Transform tParent = go.transform;
            Transform[] transforms = go.GetComponentsInChildren<Transform>(true).Where(t => t != tParent).ToArray();

            foreach (Transform t in transforms)
            {
                if (t.gameObject.name == name)
                    return t.gameObject;

                FindInactiveInternal(t.gameObject, name);
            }

            return null;
        }

        private static Boolean IsTargetObjectByName(GameObject go, string name)
        {
            return go.name == name;
        }

        private static Boolean IsTargetObjectByTag(GameObject go, string tag)
        {
            return go.CompareTag(tag);
        }
    }
}