using System.IO;
using UnityEngine;

namespace Commons.Debugging.Logging
{
    public static class FileAppendLogger
    {
        private static string _logName;

        public static void Log(string format, params object[] args)
        {
            LogF(_logName, format, args);
        }

        public static void LogF(string fileName, string format, params object[] args)
        {
            string time = string.Format("[{0} +{1}] ", Time.realtimeSinceStartup.ToString("0.000"), Time.deltaTime.ToString("0.000"));
            File.AppendAllText(fileName, string.Format(time + format + "\r\n", args));

        }

        public static void Clear(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            File.Delete(fileName);
            _logName = fileName;
        }
    }
}