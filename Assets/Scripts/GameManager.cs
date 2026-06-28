using UnityEngine;

/// <summary>
/// Manages the core game loop and general game state.
/// </summary>
public class GameManager : MonoBehaviour, IService
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
        Debug.Log("Gamemanager doing something");
    }

    /// <summary>
    /// Deinitializes the service and destroys the attached GameObject.
    /// </summary>
    public void DeInitialize() => Destroy(gameObject);
}