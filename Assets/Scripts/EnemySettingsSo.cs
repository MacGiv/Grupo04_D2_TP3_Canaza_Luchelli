using UnityEngine;

/// <summary>
/// Contains base stats and configurations for different enemy types.
/// </summary>
[CreateAssetMenu(fileName = "Enemy Settings", menuName = "Settings/Enemy", order = 1)]
public class EnemySettingsSo : ScriptableObject
{
    public string enemyName;
    public string description;
    public int health = 100;
    public int damage = 10;
    //public float speed = 100;
}