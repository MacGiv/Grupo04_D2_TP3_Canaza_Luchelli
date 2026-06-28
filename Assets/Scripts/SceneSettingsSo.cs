using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the names and references of the core scenes used in the game.
/// </summary>
[CreateAssetMenu(fileName = "Scene Settings", menuName = "Settings/Scene", order = 1)]
public class SceneSettingsSo : ScriptableObject
{
    [field: SerializeField] public string SplashScreenSceneName { get; private set; }
    [field: SerializeField] public string MainMenuSceneName { get; private set; }
    [field: SerializeField] public string GameplaySceneName { get; private set; }

#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset splashScreenScene;
    [SerializeField] private UnityEditor.SceneAsset mainMenuScene;
    [SerializeField] private UnityEditor.SceneAsset gameplayScene;

    private void OnValidate()
    {
        if (splashScreenScene != null) SplashScreenSceneName = splashScreenScene.name;
        if (mainMenuScene != null) MainMenuSceneName = mainMenuScene.name;
        if (gameplayScene != null) GameplaySceneName = gameplayScene.name;
    }
#endif
}