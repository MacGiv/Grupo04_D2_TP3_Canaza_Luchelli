using System;
using UnityEngine;

/// <summary>
/// Manages enemy spawning, tracking, and behavior across the game.
/// </summary>
public class EnemyManager : MonoBehaviour, IService
{
    /// <summary>
    /// Initializes the service with the provided game settings.
    /// </summary>
    public void Initialize(GameSettingsSo settingsSo) { }

    /// <summary>
    /// Deinitializes the service and destroys the attached GameObject.
    /// </summary>
    public void DeInitialize() => Destroy(gameObject);
}