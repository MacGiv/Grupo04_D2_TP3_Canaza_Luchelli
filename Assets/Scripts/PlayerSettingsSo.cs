using UnityEngine;

/// <summary>
/// Contains base stats and configurations for the player.
/// </summary>
[CreateAssetMenu(fileName = "Player Settings", menuName = "Settings/Player", order = 1)]
public class PlayerSettingsSo : ScriptableObject
{
    [Header("General Settings")]
    public string playerName;

    [Header("Movement Settings")]
    public int damage = 10;
    public float speed = 10;
    public float jumpForce = 15;
    public float coyoteTime = 0.15f;

    [Header("Dash Settings")]
    public float dashTime = 0.2f;
    public float dashSpeed = 25;

    [Header("Combat Settings")]
    public int health = 100;
    public float attackRadius = 0.5f;
    public float highScore = 0;
    public  LayerMask whatIsEnemy;
    public float knockbackForce = 1f;

    [Header("Sounds")]
    public AudioClip attackSound;
    public AudioClip normalDamageSound;
    public AudioClip criticalDamageSound;
    public AudioClip dashSound;

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