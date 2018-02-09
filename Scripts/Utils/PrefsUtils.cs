using Commons.Reflection;
using System;
using UnityEngine;

namespace Commons.Utils
{
    public static class PrefsUtils
    {
        public static T GetSerialized<T>(string key, bool newInstanceOnNull = true) where T : class
        {
            string value = PlayerPrefs.GetString(key, null);
            T result = SerializationUtils.BinReadFromString<T>(value);

            if (result == null && newInstanceOnNull)
                result = (T)Activator.CreateInstance(typeof(T));

            return result;
        }

        public static bool SetSerialized(string key, object obj)
        {
            string value = SerializationUtils.BinWriteToString(obj);

            if (value == null)
                return false;

            PlayerPrefs.SetString(key, value);
            return true;
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            int val = PlayerPrefs.GetInt(key, -1);

            if (val == 0)
                return false;

            if (val == 1)
                return true;

            return defaultValue;
        }

        public static void SetEnum(string key, Enum value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }

        public static T GetEnum<T>(string key, T defaultValue) where T : struct, IConvertible
        {
            string val = PlayerPrefs.GetString(key, null);

            if (string.IsNullOrEmpty(val))
                return defaultValue;

            try
            {
                return (T)Enum.Parse(typeof(T), val, true);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}