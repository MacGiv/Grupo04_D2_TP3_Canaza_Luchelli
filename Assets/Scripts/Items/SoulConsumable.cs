using UnityEngine;

/// <summary>
/// A collectible item representing currency/souls. 
/// Publishes an event when collected by the player and destroys itself.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class SoulConsumable : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("How many souls this item is worth.")]
    [SerializeField] private int soulValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventBus.Publish(new SoulCollectedEvent(soulValue));

            // TODO: Particles

            Destroy(gameObject);
        }
    }
}