using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commons.Debugging.Diagnostic;
using Commons.Utils;
using UnityEngine;

namespace Commons.Debugging
{
    public static class DebugUtils
    {
        public static void LogAwake(MonoBehaviour obj)
        {
            Track.Me(Track.EVENT, "{0} awake", TypesUtils.GetSimpleClassName(obj));
        }

        public static void LogStart(MonoBehaviour obj)
        {
            Track.Me(Track.EVENT, "{0} start", TypesUtils.GetSimpleClassName(obj));
        }

        public static string ObjectsToString<T>(IEnumerable<T> objects, int limit = 0, string separator = ",")
        {
            List<string> list = objects.Select(o => o.ToString()).ToList();

            if (limit > 0 && list.Count > limit)
                list = list.GetRange(0, limit);

            return string.Join(separator, list.ToArray());
        }

        public static string LogGameObjects<T>(string track, string prefix, IEnumerable<T> objects) where T : class
        {
            if (objects == null)
                return null;

            string[] values = new string[objects.Count()];
            int i = 0;

            foreach (T o in objects)
            {
                if (o is GameObject)
                    values[i] = (o as GameObject).name;
                else if (o is MonoBehaviour)
                    values[i] = (o as MonoBehaviour).gameObject.name;
                else if (o is Collider)
                    values[i] = (o as Collider).gameObject.name;
                else
                    throw new Exception(TypesUtils.GetSimpleClassName(o));

                i++;
            }

            return LogGameObjectsInternal(track, prefix, values);
        }

        public static string LogGameObjectsOfStruct<T>(string track, string prefix, IEnumerable<T> objects) where T : struct
        {
            if (objects == null)
                return null;

            string[] values = new string[objects.Count()];
            int i = 0;

            foreach (T o in objects)
            {
                Type type = o.GetType();

                if (type == typeof(RaycastHit))
                    values[i] = ((RaycastHit)Convert.ChangeType(o, typeof(RaycastHit))).collider.gameObject.name;
                else
                    throw new Exception(TypesUtils.GetSimpleClassName(o));

                i++;
            }

            return LogGameObjectsInternal(track, prefix, values);
        }

        public static string LogDictinary<TKey, TValue>(Dictionary<TKey, TValue> dictinary)
        {
            StringBuilder sb = new StringBuilder();

            Dictionary<TKey, TValue>.KeyCollection keys = dictinary.Keys;

            foreach (TKey key in keys)
            {
                if (sb.Length > 0)
                    sb.AppendLine();

                TValue value = U.GetValueOrThrow(dictinary, key);
                sb.Append(key).Append(": {").Append(value).Append("}");
            }

            return sb.ToString();
        }

        // Private.

        private static string LogGameObjectsInternal(string track, string prefix, string[] values)
        {
            string result;

            if (prefix == null)
                result = string.Format("[{0}] {1}", values.Length, string.Join(", ", values));
            else
                result = string.Format("{0}: [{1}] {2}", prefix, values.Length, string.Join(", ", values));

            Track.Me(track, result);
            return result;
        }
    }
}