#if !UNITY_EDITOR
using UnityEngine;
#endif
using Commons.Debugging.Diagnostic;


namespace Commons.Utils
{
    public static class AppUtils
    {
        public static void Exit(string appName)
        {
            Track.StackTrace("Exit from game: {0}", appName);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
        }
    }
}
