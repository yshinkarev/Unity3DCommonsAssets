using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Commons.Debugging.Diagnostic;

namespace Commons.Reflection
{
    public static class SerializationUtils
    {
        public static T BinReadFromFile<T>(string fileName) where T : class
        {
            if (File.Exists(fileName))
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();

                    using (FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        return (T)bf.Deserialize(f);
                    }
                }
                catch (Exception ex)
                {
                    Track.StackTrace("Bin file: {0}. Type: {1}. Error: {2}", fileName, typeof(T), ex);
                }

            return default(T);
        }

        public static T BinReadFromString<T>(string value) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return default(T);

                byte[] bytes = Convert.FromBase64String(value);

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    return (T)new BinaryFormatter().Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Track.StackTrace("Base64: {0}. Type: {1}. Error: {2}", value, typeof(T), ex.ToString());
            }

            return default(T);
        }

        public static bool BinWriteToFile(string fileName, object obj)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                using (FileStream f = new FileStream(fileName, FileMode.Create))
                {
                    bf.Serialize(f, obj);
                }

            }
            catch (Exception ex)
            {
                Track.StackTrace("Bin file: {0}. Type: {1}. Error: {2}", fileName, obj.GetType(), ex);
                return false;
            }

            return true;
        }

        public static string BinWriteToString(object obj)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, obj);
                    return Convert.ToBase64String(stream.ToArray());
                }

            }
            catch (Exception ex)
            {
                Track.StackTrace("Type: {0}. Error: {1}", obj.GetType(), ex.ToString());
                return null;
            }
        }
    }
}