using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanelController : UIPanel
{
    [Header("Button References")]
    [SerializeField] private Button backButton;

    [Header("Slider References")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Audio Mixer Reference")]
    [SerializeField] private AudioMixer audioMixer;

    private const float MIN_VOLUME = 0.0001f;
    private const float MAX_VOLUME = 1.0f;
    private const float DECIBEL_MULTIPLIER = 20.0f;

    protected override void Awake()
    {
        base.Awake();

        backButton.onClick.AddListener(HandleBackClicked);

        masterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(HandleSfxVolumeChanged);
    }

    private void Start()
    {
        masterVolumeSlider.value = AudioData.MasterVolume;
        musicVolumeSlider.value = AudioData.MusicVolume;
        sfxVolumeSlider.value = AudioData.SfxVolume;
    }

    private void HandleBackClicked() => EventBus.Publish(new BackRequestedEvent());

    private void HandleMasterVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_MASTER_VOLUME, currentValue);
        AudioData.MasterVolume = currentValue;
    }

    private void HandleMusicVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_MUSIC_VOLUME, currentValue);
        AudioData.MusicVolume = currentValue;
    }

    private void HandleSfxVolumeChanged(float currentValue)
    {
        SetVolume(AudioData.KEY_SFX_VOLUME, currentValue);
        AudioData.SfxVolume = currentValue;
    }

    private void SetVolume(string key, float value)
    {
        float volume = Mathf.Clamp(value, MIN_VOLUME, MAX_VOLUME);
        float decibels = Mathf.Log10(volume) * DECIBEL_MULTIPLIER;

        audioMixer.SetFloat(key, decibels);
    }
}