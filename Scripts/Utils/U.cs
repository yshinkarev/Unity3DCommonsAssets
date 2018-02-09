using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Commons.Debugging.Diagnostic;
using UnityEngine;

namespace Commons.Utils
{
    public static class U
    {
        public const string NULL = "null";

        private const string FLOAT_FORMAT = "0.00";

        public static int Size<T>(params T[] objects)
        {
            if (objects == null)
                return 0;

            return objects.Length;
        }

        public static int Size<T>(List<T> list)
        {
            if (list == null)
                return 0;

            return list.Count;
        }

        public static HashSet<T> Set<T>(List<T> args)
        {
            HashSet<T> result = new HashSet<T>();

            if (args == null)
                return result;

            // UC: может можно обойтись без велосипеда?
            foreach (T arg in args)
                result.Add(arg);

            return result;
        }

        public static HashSet<T> Set<T>(params T[] args)
        {
            HashSet<T> result = new HashSet<T>();

            if (args == null)
                return result;

            // UC: может можно обойтись без велосипеда?
            foreach (T arg in args)
                result.Add(arg);

            return result;
        }

        public static List<T> List<T>(params T[] args)
        {
            List<T> result = new List<T>();

            if (args == null)
                return result;

            result.AddRange(args);

            return result;
        }

        public static T Last<T>(T[] array)
        {
            if (array == null)
                return default(T);

            return array.Last();
        }

        public static T ParseEnum<T>(string value, T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            if (string.IsNullOrEmpty(value))
                return defaultValue;

            string expected = value.Trim().ToLower();

            foreach (T item in Enum.GetValues(typeof(T)))
                if (item.ToString().ToLower().Equals(expected)) return item;

            return defaultValue;
        }

        /// <returns>Value or throw NullReferenceException/ArgumentException</returns>
        public static TValue GetValueOrThrow<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (key == null)
                throw new NullReferenceException();

            TValue result;

            if (dictionary.TryGetValue(key, out result))
                return result;

            throw new ArgumentException(key.ToString());
        }

        public static TValue GetValueOrNull<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (key == null)
                throw new NullReferenceException();

            TValue result;

            if (dictionary.TryGetValue(key, out result))
                return result;

            return default(TValue);
        }

        public static IEnumerable<T> GetEnumValues<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T[] GetEnumValuesArray<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public static String AddSpaceForCapsWords(string text)
        {
            return Regex.Replace(text, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
        }

        public static Dictionary<TKey, TValue> CloneDictionaryCloningValues<TKey, TValue>(Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);

            foreach (KeyValuePair<TKey, TValue> entry in original)
                ret.Add(entry.Key, (TValue)entry.Value.Clone());

            return ret;
        }

        ///////////////////////////////// Unity3D. /////////////////////////////////

        public static Color Color(Color src)
        {
            return new Color { a = src.a, r = src.r, b = src.b, g = src.g };
        }

        public static string RaycastHitToString(RaycastHit hit)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Dist: ").Append(F(hit.distance)).Append(". Point: ").Append(hit.point)/*.Append(". Obj:").Append(hit.collider.gameObject)*/;
            return sb.ToString();
        }

        public static string F(float value)
        {
            return value.ToString(FLOAT_FORMAT);
        }

        public static string F(Vector2 v)
        {
            return F(v.x) + ";" + F(v.y);
        }

        public static string F(Vector3 v)
        {
            return F(v.x) + ";" + F(v.y) + ";" + F(v.z);
        }

        public static void CollideWith(MonoBehaviour src, Collider collider)
        {
            Track.Me(Track.DEV, "{0} collide with {1}", src.gameObject.name, collider.gameObject.name);
        }

        public static void CollideWith(MonoBehaviour src, Collision collision)
        {
            CollideWith(src, collision.collider);
        }

        public static string LogMonoBehaviour(MonoBehaviour mb)
        {
            GameObject go = mb.gameObject;
            StringBuilder sb = new StringBuilder();

            if (go.name == null)
                sb.Append(mb);
            else
                sb.Append(go.name);

            return sb.ToString();
        }

        public static Vector3 GetSize(MonoBehaviour obj)
        {
            return obj.GetComponentInChildren<Renderer>().bounds.size;
        }

        public static Vector3 GetSize(GameObject go)
        {
            return go.GetComponentInChildren<Renderer>().bounds.size;
        }

        public static string GetName(UnityEngine.Object go)
        {
            return (go == null) ? NULL : go.name;
        }

        public static IEnumerator WaitForRealSeconds(float seconds)
        {
            // Time.scale = 0f, WaitForSeconds do not work.
            float target = Time.realtimeSinceStartup + seconds;

            while (Time.realtimeSinceStartup < target)
                yield return null;
        }
    }
}