using UnityEngine;
using UnityEngine.Audio;

namespace InventixGames.Core
{
    public interface IAudioService
    {
        void PlaySfx(AudioClip clip, float volume = 1f);
        void PlayMusic(AudioClip clip, bool loop = true);
        void StopMusic();
        void SetMasterVolume(float zeroToOne);
    }

    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        private void Awake()
        {
            if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
            if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false; sfxSource.playOnAwake = false;
        }

        public void PlaySfx(AudioClip clip, float volume = 1f) { if (clip != null) sfxSource.PlayOneShot(clip, volume); }
        public void PlayMusic(AudioClip clip, bool loop = true) { if (clip == null) return; musicSource.clip = clip; musicSource.loop = loop; musicSource.Play(); }
        public void StopMusic() => musicSource.Stop();
        public void SetMasterVolume(float v) { if (mixer != null) mixer.SetFloat("MasterVolume", Mathf.Lerp(-80f, 0f, Mathf.Clamp01(v))); else AudioListener.volume = Mathf.Clamp01(v); }
    }
}
