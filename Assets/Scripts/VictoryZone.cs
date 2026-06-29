using UnityEngine;

/// <summary>
/// A trigger zone that completes the level when the player enters it.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class VictoryZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("VICTORY!");
            // Disparamos el evento de victoria
            EventBus.Publish(new GameOverEvent(true));
        }
    }
}