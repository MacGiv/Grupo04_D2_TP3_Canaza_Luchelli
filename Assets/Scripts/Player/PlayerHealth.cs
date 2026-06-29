using System.Collections;
using UnityEngine;

/// <summary>
/// Manages player's health, damage reception, and temporary invulnerability (i-frames) after getting hit.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("I-Frames Settings")]
    public float invulnerabilityDuration = 1.5f; 
    public float flashInterval = 0.1f;

    private PlayerBrain playerBrain;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerBrain = GetComponent<PlayerBrain>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Called by enemies or hazards to deal damage to the player.
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (playerBrain.IsInvulnerable) return;

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            playerBrain.StateMachine.ChangeState(playerBrain.HitState);
            StartCoroutine(DamageRecoveryRoutine());
        }
    }

    /// <summary>
    /// Handles the i-frames and the blinking visual effect.
    /// </summary>
    private IEnumerator DamageRecoveryRoutine()
    {
        playerBrain.IsInvulnerable = true;

        float elapsedTime = 0f;
        bool isSpriteVisible = true;

        // Blinking
        while (elapsedTime < invulnerabilityDuration)
        {
            isSpriteVisible = !isSpriteVisible;
            spriteRenderer.enabled = isSpriteVisible;

            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        // Reset player's parameters (just in case)
        spriteRenderer.enabled = true;
        playerBrain.IsInvulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player is DEAD!");
        playerBrain.StateMachine.ChangeState(playerBrain.DeathState);
    }
}