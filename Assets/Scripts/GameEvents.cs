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

public struct TutorialToggleEvent
{
    public string Message;
    public Sprite TutorialSprite;
    public bool Show;

    public TutorialToggleEvent(string message, Sprite tutorialSprite, bool show)
    {
        Message = message;
        TutorialSprite = tutorialSprite;
        Show = show;
    }
}

public struct GameOverEvent
{
    public bool IsVictory;
    public GameOverEvent(bool isVictory) => IsVictory = isVictory;
}