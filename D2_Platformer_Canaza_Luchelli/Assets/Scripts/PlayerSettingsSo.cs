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
    public float speed = 100;
    public int maxRetries = 3;
    public int highScore = 0;

    /// <summary>
    /// Initializes player settings loading saved data.
    /// </summary>
    public void Initialize()
    {
        LoadSettings();
    }

    /// <summary>
    /// Loads the player's high score.
    /// </summary>
    private void LoadSettings()
    {
        highScore = PlayerPrefs.GetInt("highScore", 0);
    }
}