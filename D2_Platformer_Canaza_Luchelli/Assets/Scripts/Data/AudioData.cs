using UnityEngine;

/// <summary>
/// Provides persistent storage for audio volume settings using PlayerPrefs.
/// Values are saved to disk on every set. Default volume is 1.0 (100%).
/// </summary>
public static class AudioData
{
    public const string KEY_MASTER_VOLUME = "MasterVolume";
    public const string KEY_MUSIC_VOLUME = "MusicVolume";
    public const string KEY_SFX_VOLUME = "SfxVolume";

    private const float DEFAULT_VOLUME = 1.0f;

    public static float MasterVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(KEY_MASTER_VOLUME, DEFAULT_VOLUME);
        }
        set
        {
            PlayerPrefs.SetFloat(KEY_MASTER_VOLUME, value);
            PlayerPrefs.Save();
        }
    }

    public static float MusicVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, DEFAULT_VOLUME);
        }
        set
        {
            PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, value);
            PlayerPrefs.Save();
        }
    }

    public static float SfxVolume
    {
        get
        {
            return PlayerPrefs.GetFloat(KEY_SFX_VOLUME, DEFAULT_VOLUME);
        }
        set
        {
            PlayerPrefs.SetFloat(KEY_SFX_VOLUME, value);
            PlayerPrefs.Save();
        }
    }
}