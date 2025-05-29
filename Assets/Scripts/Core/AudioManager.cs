using UnityEngine;

namespace Core
{
    public class AudioManager : Singleton<AudioManager>, IInitializable
    {
        private const string BACKGROUND_MUSIC_PATH = "Sounds/Birds";
        private AudioSource backgroundMusic;
        private float volume = 1f;
        private GameObject audioSourceObject;

        public void Initialize()
        {
            audioSourceObject = new GameObject("AudioManager");
            Object.DontDestroyOnLoad(audioSourceObject);
            InitializeAudio();
        }

        private void InitializeAudio()
        {
            backgroundMusic = audioSourceObject.AddComponent<AudioSource>();
            backgroundMusic.loop = true;
            backgroundMusic.playOnAwake = false;
        
            AudioClip birdsSound = Resources.Load<AudioClip>(BACKGROUND_MUSIC_PATH);
            if (birdsSound != null)
            {
                backgroundMusic.clip = birdsSound;
                PlayBackgroundMusic();
            }
            else
            {
                Debug.LogError("Failed to load Birds.mp3");
            }
        }

        public void PlayBackgroundMusic()
        {
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }

        public void StopBackgroundMusic()
        {
            if (backgroundMusic != null && backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }
        }

        public void SetVolume(float newVolume)
        {
            volume = Mathf.Clamp01(newVolume);
            if (backgroundMusic != null)
            {
                backgroundMusic.volume = volume;
            }
        }

        public float GetVolume()
        {
            return volume;
        }
    }
} 