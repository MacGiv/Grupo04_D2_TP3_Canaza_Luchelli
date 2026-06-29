using UnityEngine;

/// <summary>
/// Manages the core game loop, global game state, and persistent resources like Souls.
/// </summary>
public class GameManager : MonoBehaviour, IService
{
    public int TotalSouls { get; private set; }

    public void Initialize(GameSettingsSo settingsSo)
    {
        EventBus.Subscribe<SoulCollectedEvent>(OnSoulCollected);
    }

    public void DeInitialize()
    {
        EventBus.Unsubscribe<SoulCollectedEvent>(OnSoulCollected);
        Destroy(gameObject);
    }

    /// <summary>
    /// Triggered when a SoulPickup is collected in the world.
    /// </summary>
    private void OnSoulCollected(SoulCollectedEvent eventData)
    {
        TotalSouls += eventData.Amount;
        Debug.Log($"Collected {eventData.Amount} souls. Total: {TotalSouls}");

        EventBus.Publish(new UIUpdateSoulsEvent(TotalSouls));
    }
}