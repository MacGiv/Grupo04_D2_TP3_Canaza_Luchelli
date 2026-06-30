using UnityEngine;

/// <summary>
/// Handles combat mechanics, hitboxes, and damage detection for the player.
/// </summary>
public class PlayerCombatHandler : MonoBehaviour
{
    [Header("Hitbox Settings")]
    [SerializeField] private Transform attackPoint;

    private PlayerBrain player;
    private float attackRadius = 0.5f;
    private LayerMask enemyLayer;
    private float knockbackForce = 1f;

    private void Awake()
    {
        player = GetComponent<PlayerBrain>();
        if (player != null)
        {
            attackRadius = player.PlayerSettings.attackRadius;
            enemyLayer = player.PlayerSettings.whatIsEnemy;
            knockbackForce = player.PlayerSettings.knockbackForce;
        }
        else
            Debug.LogWarning("PlayerBrain not found.");

    }

    /// <summary>
    /// Detects and damages enemies within the attack radius.
    /// Gets combo step to apply special effects
    /// </summary>
    public void CheckMeleeHit(int comboStep)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);
        Vector2 knockbackDir = new Vector2(GetComponent<PlayerMovementHandler>().FacingDirection, 0.2f).normalized;

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                if (comboStep == 1 || comboStep == 2)
                {
                    enemyHealth.TakeDamage(10, knockbackDir, knockbackForce);
                    Debug.Log($"Normal attack to {enemy.name} (Combo Step {comboStep}).");

                    EventBus.Publish(new SfxRequestedEvent { clip = player.PlayerSettings.normalDamageSound });
                }
                else if (comboStep == 3)
                {
                    enemyHealth.TakeDamage(25, knockbackDir, knockbackForce*3);
                    Debug.Log($"¡Special Attack to {enemy.name}! (Combo Step {comboStep}).");

                    EventBus.Publish(new SfxRequestedEvent { clip = player.PlayerSettings.criticalDamageSound });
                }
            }
        }
    }

    /// <summary>
    /// Draws the hitbox in the Unity Editor.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}