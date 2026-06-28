using UnityEngine;

/// <summary>
/// Holds the user's specific preferences.
/// </summary>
[CreateAssetMenu(fileName = "User Settings", menuName = "Settings/User", order = 1)]
public class UserSettingsSo : ScriptableObject
{
    public float masterVolume = 100;
    public float musicVolume = 100;
    public float sfxVolume = 100;
    public float uiVolume = 100;

    /// <summary>
    /// Initializes user settings by loading saved preferences.
    /// </summary>
    public void Initialize()
    {
        LoadSettings();
    }

    /// <summary>
    /// Loads settings from PlayerPrefs.
    /// </summary>
    private void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("masterVolume", 100);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 100);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 100);
        uiVolume = PlayerPrefs.GetFloat("uiVolume", 100);
    }
}