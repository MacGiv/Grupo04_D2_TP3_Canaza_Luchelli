using UnityEngine;

/// <summary>
/// Handles all the audio playback, including music and sound effects.
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

    /// <summary>
    /// Performs a generic action for testing purposes.
    /// </summary>
    public void DoSomething()
    {
        Debug.Log("AudioManager doing something");
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