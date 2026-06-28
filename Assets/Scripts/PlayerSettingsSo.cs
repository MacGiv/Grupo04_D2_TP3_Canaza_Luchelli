using UnityEngine;

/// <summary>
/// Contains base stats and configurations for the player.
/// </summary>
[CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player", order = 1)]
public class PlayerSettingsSo : ScriptableObject
{
    public string playerName;
    public int health = 100;
    public int damage = 10;
    public float speed = 10;
    public float jumpForce = 15;
    public float coyoteTime = 0.15f;
    public float highScore = 0;

    /// <summary>
    /// Initializes player settings loading saved data.
    /// </summary>
    public void Initialize()
    {
        LoadSettings();
    }

    /// <summary>
    /// Loads the player's settings. WIP
    /// </summary>
    private void LoadSettings()
    {

    }
}