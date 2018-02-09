using UnityEngine;

namespace Commons.Utils
{
    public static class AudioUtils
    {
        public static void Play(MonoBehaviour script, AudioClip clip)
        {
            AudioSource a = script.GetComponent<AudioSource>();
            a.clip = clip;
            a.Play();
        }

        public static void PauseAllSources()
        {
            foreach (AudioSource asrc in GetAudioSourcesOfSystem())
                asrc.Pause();
        }

        public static void StopAllSources()
        {
            foreach (AudioSource asrc in GetAudioSourcesOfSystem())
                asrc.Stop();
        }

        public static void UnPauseAllSources()
        {
            foreach (AudioSource asrc in GetAudioSourcesOfSystem())
                asrc.UnPause();
        }

        public static void SetVolume(float volume)
        {
            foreach (AudioSource asrc in GetAudioSourcesOfSystem())
                asrc.volume = volume;
        }

        public static AudioSource[] GetAudioSourcesOfSystem()
        {
            return UnityEngine.Object.FindObjectsOfType<AudioSource>();
        }
    }
}