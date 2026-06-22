using UnityEngine;

/// <summary>
/// Handles combat mechanics, hitboxes, and damage detection for the player.
/// </summary>
public class PlayerCombatHandler : MonoBehaviour
{
    [Header("Hitbox Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask whatIsEnemy;

    /// <summary>
    /// Detects and damages enemies within the attack radius.
    /// This method will be called via Animation Events.
    /// </summary>
    public void CheckMeleeHit()
    {
        // Get objects in attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsEnemy);
        foreach (Collider2D enemy in hitEnemies)
        {
            // TODO: "Take Damage" 
            Debug.Log($"SI BOKITA PIERDE, PIERDE: {enemy.name}!");
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