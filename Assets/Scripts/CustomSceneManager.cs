using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions, loading and unloading scenes additively.
/// </summary>
public class CustomSceneManager : MonoBehaviour, IService
{
    private SceneSettingsSo settingsSo;

    /// <summary>
    /// List of currently active scenes.
    /// </summary>
    public List<string> currentScenes = new List<string>();

    /// <summary>
    /// Initializes the scene manager and caches the scene settings.
    /// </summary>
    public void Initialize(GameSettingsSo settingsSo)
    {
        this.settingsSo = settingsSo.SceneSettingsSo;
    }

    /// <summary>
    /// Loads the splash screen scene.
    /// </summary>
    public void GoToSplashScreen()
    {
        LoadScene(settingsSo.SplashScreenSceneName, null);
    }

    /// <summary>
    /// Transitions from the splash screen to the main menu.
    /// </summary>
    public void GoToMainMenuFromSplashScreen() =>
            LoadScene(settingsSo.MainMenuSceneName, settingsSo.SplashScreenSceneName);

    /// <summary>
    /// Transitions from the gameplay scene to the main menu.
    /// </summary>
    public void GoToMainMenuFromGameplay() => LoadScene(settingsSo.MainMenuSceneName, settingsSo.GameplaySceneName);

    /// <summary>
    /// Transitions from the main menu to the gameplay scene.
    /// </summary>
    public void GoToGameplay() => LoadScene(settingsSo.GameplaySceneName, settingsSo.MainMenuSceneName);

    /// <summary>
    /// Restarts the current gameplay scene.
    /// </summary>
    public void RestartGameplay()
    {
        LoadScene(settingsSo.GameplaySceneName, settingsSo.GameplaySceneName);
    }

    /// <summary>
    /// Internal method to load a scene additively and optionally unload a previous one.
    /// </summary>
    private void LoadScene(string loadSceneName, string unloadSceneName)
    {
        currentScenes.Add(loadSceneName);
        SceneManager.LoadScene(loadSceneName, LoadSceneMode.Additive);

        if (!string.IsNullOrEmpty(unloadSceneName))
        {
            currentScenes.Remove(unloadSceneName);
            SceneManager.UnloadSceneAsync(unloadSceneName);
        }
    }

    /// <summary>
    /// Deinitializes the service and destroys the attached GameObject.
    /// </summary>
    public void DeInitialize() => Destroy(gameObject);
}