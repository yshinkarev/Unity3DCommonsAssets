using System;
using System.IO;
using System.Text;

namespace Commons.Utils
{
    // ReSharper disable once InconsistentNaming
    public static class IOUtils
    {
        public static bool DeleteFile(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;

            try
            {
                File.Delete(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AppendAllText(String path, String contents)
        {
            try
            {
                File.AppendAllText(path, contents);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool AppendAllText(String path, String contents, Encoding encoding)
        {
            try
            {
                File.AppendAllText(path, contents, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}