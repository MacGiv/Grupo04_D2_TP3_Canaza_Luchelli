using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions safely and sequentially using Coroutines to avoid duplication bugs.
/// </summary>
public class CustomSceneManager : MonoBehaviour, IService
{
    private SceneSettingsSo settingsSo;

    /// <summary>
    /// List of currently active scenes.
    /// </summary>
    public List<string> currentScenes = new List<string>();

    public void Initialize(GameSettingsSo settingsSo)
    {
        this.settingsSo = settingsSo.SceneSettingsSo;
    }

    public void GoToSplashScreen() => TransitionToScene(settingsSo.SplashScreenSceneName, null);
    public void GoToMainMenuFromSplashScreen() => TransitionToScene(settingsSo.MainMenuSceneName, settingsSo.SplashScreenSceneName);
    public void GoToMainMenuFromGameplay() => TransitionToScene(settingsSo.MainMenuSceneName, settingsSo.GameplaySceneName);
    public void GoToGameplay() => TransitionToScene(settingsSo.GameplaySceneName, settingsSo.MainMenuSceneName);
    public void RestartGameplay() => TransitionToScene(settingsSo.GameplaySceneName, settingsSo.GameplaySceneName);

    /// <summary>
    /// Helper method to safely kick off the transition coroutine.
    /// </summary>
    private void TransitionToScene(string loadSceneName, string unloadSceneName)
    {
        StartCoroutine(SafeTransitionRoutine(loadSceneName, unloadSceneName));
    }

    private IEnumerator SafeTransitionRoutine(string loadSceneName, string unloadSceneName)
    {
        Time.timeScale = 1f;

        // 1. If there's a previous scene loaded, we unload it before loading next scene
        if (!string.IsNullOrEmpty(unloadSceneName))
        {
            if (currentScenes.Contains(unloadSceneName))
            {
                currentScenes.Remove(unloadSceneName);
            }

            yield return SceneManager.UnloadSceneAsync(unloadSceneName);
            yield return null; // Frame extra for Garbage Collector just in case
        }

        // 2. Load new scene
        currentScenes.Add(loadSceneName);
        yield return SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);

        // 3. Establish new scene as active
        Scene newlyLoadedScene = SceneManager.GetSceneByName(loadSceneName);
        if (newlyLoadedScene.IsValid())
        {
            SceneManager.SetActiveScene(newlyLoadedScene);
        }

        Debug.Log($"CustomSceneManager: Transition successfully made to: {loadSceneName}");
    }

    public void DeInitialize() => Destroy(gameObject);
}