using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Handles all audio playback in the game. Registered in the ServiceLocator.
/// </summary>
public class AudioManager : MonoBehaviour, IService
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    /// <summary>
    /// Initializes the service with the provided game settings.
    /// </summary>
    public void Initialize(GameSettingsSo settingsSo) 
    {
        musicSource.volume = AudioData.MasterVolume;
        sfxSource.volume = AudioData.SfxVolume;

        EventBus.Subscribe<SfxRequestedEvent>(OnSFXRequested);
    }

    private void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = 1f;
    }

    /// <summary>
    /// Deinitializes the service and destroys the attached GameObject.
    /// </summary>
    public void DeInitialize()
    {
        EventBus.Unsubscribe<SfxRequestedEvent>(OnSFXRequested);

        Destroy(gameObject);
    }

    private void OnSFXRequested(SfxRequestedEvent sfxRequestedEvent) 
        => sfxSource.PlayOneShot(sfxRequestedEvent.clip);
}