using UnityEngine;

namespace Commons.Helper
{
    public class AudioHelper
    {
        private readonly AudioSource _audioSource;

        public AudioHelper(GameObject go)
        {
            _audioSource = go.GetComponent<AudioSource>();
        }

        public AudioHelper(MonoBehaviour script)
        {
            _audioSource = script.GetComponent<AudioSource>();
        }

        public AudioHelper(AudioSource source)
        {
            _audioSource = source;
        }

        public void Play(AudioClip clip)
        {
            if (_audioSource.clip == clip)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();

                return;
            }

            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Pause()
        {
            if (_audioSource.clip != null)
                _audioSource.Pause();
        }

        public void Stop(AudioClip clip = null)
        {
            if (_audioSource.clip == null)
                return;

            if (clip != null && _audioSource.clip != clip)
                return;

            _audioSource.Stop();
        }

        public bool Enabled
        {
            set { _audioSource.enabled = value; }
            get { return _audioSource.enabled; }
        }
    }
}