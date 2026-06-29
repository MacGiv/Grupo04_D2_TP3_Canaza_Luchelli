using UnityEngine;

/// <summary>
/// A consumable item that restores player health upon collision.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class HealthConsumable : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Amount of health this item restores.")]
    [SerializeField] private int healAmount = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);

                // TODO: Particles

                Destroy(gameObject);
            }
        }
    }
}