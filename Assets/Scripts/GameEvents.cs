using UnityEngine;

public struct PlayerHealthChangedEvent
{
    public int CurrentHealth;
    public int MaxHealth;

    public PlayerHealthChangedEvent(int currentHealth, int maxHealth)
    {
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
    }
}

public struct SoulCollectedEvent
{
    public int Amount;
    public SoulCollectedEvent(int amount) => Amount = amount;
}

public struct UIUpdateSoulsEvent
{
    public int TotalSouls;
    public UIUpdateSoulsEvent(int totalSouls) => TotalSouls = totalSouls;
}

/// <summary>
/// Dispatched when a player enters or exits a tutorial trigger zone.
/// </summary>
public struct TutorialEvent
{
    public TutorialDataSo Data;
    public bool Show;

    public TutorialEvent(TutorialDataSo data, bool show)
    {
        Data = data;
        Show = show;
    }
}

public struct GameOverEvent
{
    public bool IsVictory;
    public GameOverEvent(bool isVictory) => IsVictory = isVictory;
}