using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Источники звука")]
    public AudioSource soundEffectsSource;
    public AudioSource musicSource;

    [Header("Настройки громкости")]
    [Range(0f, 1f)] public float soundEffectsVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;

    public AudioClip[] soundEffectsClips;
    public AudioClip[] musicClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Initialize();
    }

    public void Initialize()
    {
        if (soundEffectsSource == null)
        {
            GameObject soundEffectsObject = new GameObject("SoundEffectsSource");
            soundEffectsSource = soundEffectsObject.AddComponent<AudioSource>();
            soundEffectsObject.transform.SetParent(transform);
        }

        if (musicSource == null)
        {
            GameObject musicObject = new GameObject("MusicSource");
            musicSource = musicObject.AddComponent<AudioSource>();
            musicObject.transform.SetParent(transform);
            musicSource.loop = true;
        }

        SetSoundEffectsVolume(soundEffectsVolume);
        SetMusicVolume(musicVolume);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null && soundEffectsSource != null)
        {
            soundEffectsSource.PlayOneShot(clip);
        }
    }

    public void PlaySoundEffect(string clipName)
    {
        AudioClip clipToPlay = FindSoundEffectClip(clipName);
        if (clipToPlay != null)
        {
            PlaySoundEffect(clipToPlay);
        }
        else
        {
            Debug.LogWarning("Звуковой эффект '" + clipName + "' не найден.");
        }
    }

    private AudioClip FindSoundEffectClip(string clipName)
    {
        if (soundEffectsClips == null || soundEffectsClips.Length == 0) return null;
        foreach (AudioClip clip in soundEffectsClips)
        {
            if (clip != null && clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlayMusic(string clipName)
    {
        AudioClip clipToPlay = FindMusicClip(clipName);
        if (clipToPlay != null)
        {
            PlayMusic(clipToPlay);
        }
        else
        {
            Debug.LogWarning("Музыкальный трек '" + clipName + "' не найден.");
        }
    }

    private AudioClip FindMusicClip(string clipName)
    {
        if (musicClips == null || musicClips.Length == 0) return null;
        foreach (AudioClip clip in musicClips)
        {
            if (clip != null && clip.name == clipName)
            {
                return clip;
            }
        }
        return null;
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = Mathf.Clamp01(volume);
        if (soundEffectsSource != null)
        {
            soundEffectsSource.volume = soundEffectsVolume;
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}