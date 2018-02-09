using System;
using System.Collections.Generic;
using Commons.Utils;
using UnityEngine;

namespace Commons.Debugging.Diagnostic
{
    public class Track
    {
        public const string DEV = "Dev";
        public const string SETTINGS = "Settings";
        public const string ADS = "Ads";
        public const string GOOGLE_API = "GoogleApi";
        public const string EVENT = "Event";
        public const string UI = "IOUtils";
        public const string POOL = "Pool";
        public const string NATIVE = "Native";
        public const string STACK_TRACE = "StackTrace";

        public static List<string> ReleaseTracks
        {
            get { return U.List(STACK_TRACE); }
        }

        private static HashSet<string> _whatToTrack = new HashSet<string>();
        private static string _logFileName;

        public static void SetTracks(List<String> tracks)
        {
            _whatToTrack = U.Set(tracks);
        }

        public static void SetLogFileName(string filename)
        {
            _logFileName = filename;
            IOUtils.DeleteFile(filename);
        }

        public static Boolean IsEnabled(String track)
        {
            return _whatToTrack.Contains(track);
        }

        public static Boolean IsDisabled(String track)
        {
            return !_whatToTrack.Contains(track);
        }

        public static void Me(String track, String format, params object[] args)
        {
            if (!_whatToTrack.Contains(track))
                return;

            string text = (args.Length == 0) ? format : string.Format(format, args);

            Debug.Log(text);

            if (!string.IsNullOrEmpty(_logFileName))
                IOUtils.AppendAllText(_logFileName, text + Environment.NewLine);
        }

        public static void Dev(String format, params object[] args)
        {
            Me(DEV, format, args);
        }

        public static void StackTrace(String format, params object[] args)
        {
            Me(STACK_TRACE, format, args);
        }
    }
}
