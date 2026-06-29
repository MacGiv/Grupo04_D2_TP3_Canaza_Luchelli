using UnityEngine;

/// <summary>
/// Attached to an enemy attack hitbox or hazard with a Collider2D set as a Trigger.
/// Detects when the player enters the zone and inflicts damage based on the enemy's stats.
/// </summary>
public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] int alternativeDamage = 10;
    private EnemyBrain enemyBrain;

    private void Awake()
    {
        enemyBrain = GetComponentInParent<EnemyBrain>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // If we found an enemy brain, we use it's damage
                // If not (example: trap/hazard) we use an alternative value (default or preconfigured)
                int damageToDeal = (enemyBrain != null) ? enemyBrain.Settings.damage : alternativeDamage;

                playerHealth.TakeDamage(damageToDeal);
            }
        }
    }
}