using UnityEngine;

/// <summary>
/// Contains base stats and configurations for different enemy types.
/// </summary>
[CreateAssetMenu(fileName = "New Enemy Settings", menuName = "Settings/Enemy", order = 1)]
public class EnemySettingsSo : ScriptableObject
{
    [Header("General Info")]
    public string enemyName;
    public string description;
    public int maxHealth = 100;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4.5f;

    [Header("Detection & Combat")]
    public float visionRange = 7f;   
    public float attackRange = 1.5f; 
    public float attackCooldown = 2f;
    public int damage = 10;
}