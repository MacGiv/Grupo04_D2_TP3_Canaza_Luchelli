using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Listens to player health events via EventBus and drives the UI Health Bar display.
/// </summary>
public class UIHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;

    private void OnEnable()
    {
        EventBus.Subscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<PlayerHealthChangedEvent>(OnPlayerHealthChanged);
    }

    /// <summary>
    /// Callback triggered whenever the player's health changes. Updates the slider value.
    /// </summary>
    private void OnPlayerHealthChanged(PlayerHealthChangedEvent eventData)
    {
        if (healthSlider != null)
        {
            // Calculate normalized health value
            float healthPercentage = (float)eventData.CurrentHealth / eventData.MaxHealth;
            healthSlider.value = healthPercentage;
        }
    }
}