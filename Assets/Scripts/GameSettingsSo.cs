using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The master settings object.
/// </summary>
[CreateAssetMenu(fileName = "Game Settings", menuName = "Settings/Game", order = 1)]
public class GameSettingsSo : ScriptableObject
{
    public UserSettingsSo UserSettingsSo;
    public PlayerSettingsSo PlayerSettingsSo;
    public SceneSettingsSo SceneSettingsSo;
    public List<EnemySettingsSo> EnemySettingsSo = new List<EnemySettingsSo>();

    /// <summary>
    /// Initializes all sub-settings objects.
    /// </summary>
    public void Initialize()
    {
        UserSettingsSo.Initialize();
        PlayerSettingsSo.Initialize();
        // SceneSettingsSo.Initialize();
        // EnemySettingsSo.Initialize();
    }
}