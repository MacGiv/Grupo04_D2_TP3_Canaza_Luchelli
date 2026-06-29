using UnityEngine;

/// <summary>
/// Mock bootstrapper used to isolate and test the gameplay scene without going through the main Boot sequence.
/// Destroys itself if the real Bootstrapper is detected.
/// </summary>
public class Test_Bootstrapper : MonoBehaviour
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
        // Corregido: Buscamos si existe el real. Si existe, este de pruebas se autodestruye.
        if (FindFirstObjectByType<Bootstrapper>() != null)
        {
            Debug.Log("Test_Bootstrapper: Se detectó el Bootstrapper real. Destruyendo mock de pruebas.");
            Destroy(gameObject);
            return; // Corta el Awake acá para el clon
        }

        // Validación de seguridad por si te olvidás de arrastrar el asset en el Inspector
        if (gameSettings == null)
        {
            Debug.LogError("Test_Bootstrapper: ¡Falta asignar el GameSettingsSo en el Inspector de este objeto!");
            return;
        }

        // Ahora sí, si estamos testeando la escena suelta, inicializa todo el Service Locator:
        gameSettings.Initialize();

        ServiceLocator.DeInitializeServices();
        ServiceLocator.ClearServices();

        ServiceLocator.AddService(new GameObject("Game Manager").AddComponent<GameManager>());
        ServiceLocator.AddService(new GameObject("Custom Scene Manager").AddComponent<CustomSceneManager>());
        ServiceLocator.AddService(new GameObject("Audio Manager").AddComponent<AudioManager>());
        ServiceLocator.AddService(new GameObject("Enemy Manager").AddComponent<EnemyManager>());

        ServiceLocator.InitializeServices(gameSettings);

        Debug.Log("Test_Bootstrapper: ¡Sistemas inicializados con éxito para modo Test!");
    }
}