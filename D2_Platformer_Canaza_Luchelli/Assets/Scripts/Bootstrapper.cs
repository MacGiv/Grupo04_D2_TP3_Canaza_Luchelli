using UnityEngine;

/// <summary>
/// Responsible for initializing the core systems and registering services into the Service Locator.
/// </summary>
public class Bootstrapper : MonoBehaviour
{
    /// <summary>
    /// Reference to the global game settings.
    /// </summary>
    [SerializeField] private GameSettingsSo gameSettings;

    /// <summary>
    /// Clears existing services, instantiates new manager GameObjects, and registers them.
    /// </summary>
    private void Awake()
    {
        gameSettings.Initialize();

        ServiceLocator.DeInitializeServices();
        ServiceLocator.ClearServices();

        ServiceLocator.AddService(new GameObject("Game Manager").AddComponent<GameManager>());
        ServiceLocator.AddService(new GameObject("Custom Scene Manager").AddComponent<CustomSceneManager>());
        ServiceLocator.AddService(new GameObject("Audio Manager").AddComponent<AudioManager>());
        ServiceLocator.AddService(new GameObject("Enemy Manager").AddComponent<EnemyManager>());

        ServiceLocator.InitializeServices(gameSettings);
    }

    /// <summary>
    /// Triggers the initial scene transition after all systems are initialized.
    /// </summary>
    private void Start()
    {
        ServiceLocator.GetService<CustomSceneManager>().GoToSplashScreen();
    }
}