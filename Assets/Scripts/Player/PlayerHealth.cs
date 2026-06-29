using System.Collections;
using UnityEngine;

/// <summary>
/// Manages player's health, damage reception, healing, and triggers health events via EventBus.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("I-Frames Settings")]
    [SerializeField] private float invulnerabilityDuration = 1.5f;
    [SerializeField] private float flashInterval = 0.1f;

    private PlayerBrain playerBrain;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerBrain = GetComponent<PlayerBrain>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        PublishHealthUpdate();
    }

    /// <summary>
    /// Restores health to the player up to the maximum health limit.
    /// </summary>
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"Player healed by {amount}! Current HP: {currentHealth}");

        PublishHealthUpdate();
    }

    public void TakeDamage(int damage)
    {
        if (playerBrain.IsInvulnerable || currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage! Current HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PublishHealthUpdate();
            Die();
        }
        else
        {
            playerBrain.StateMachine.ChangeState(playerBrain.HitState);
            PublishHealthUpdate();
            StartCoroutine(DamageRecoveryRoutine());
        }
    }

    /// <summary>
    /// Method to centralize publishing health data to the EventBus.
    /// </summary>
    private void PublishHealthUpdate()
    {
        EventBus.Publish(new PlayerHealthChangedEvent(currentHealth, maxHealth));
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        playerBrain.IsInvulnerable = true;
        float elapsedTime = 0f;
        bool isSpriteVisible = true;

        while (elapsedTime < invulnerabilityDuration)
        {
            isSpriteVisible = !isSpriteVisible;
            spriteRenderer.enabled = isSpriteVisible;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        spriteRenderer.enabled = true;
        playerBrain.IsInvulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player is DEAD!");
        playerBrain.StateMachine.ChangeState(playerBrain.DeathState);
    }
}