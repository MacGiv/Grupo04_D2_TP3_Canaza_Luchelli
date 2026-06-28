using UnityEngine;

/// <summary>
/// Handles all the audio playback, including music and sound effects.
/// </summary>
public class AudioManager : MonoBehaviour, IService
{
    /// <summary>
    /// Initializes the service with the provided game settings.
    /// </summary>
    public void Initialize(GameSettingsSo settingsSo) { }

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
    public void DeInitialize() => Destroy(gameObject);
}