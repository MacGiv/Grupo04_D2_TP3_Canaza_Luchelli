using UnityEngine;

/// <summary>
/// Coordinates navigation between main menu panels.
/// Listens to UI events and manages panel visibility.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    [Header("Panels References")]
    [SerializeField] private MainPanelController mainPanel;
    [SerializeField] private SettingsPanelController settingsPanel;
    [SerializeField] private CreditsPanelController creditsPanel;

    private void Awake()
    {
        EventBus.Subscribe<PlayRequestedEvent>(OnPlayPressed);
        EventBus.Subscribe<SettingsRequestedEvent>(OnSettingsPressed);
        EventBus.Subscribe<CreditsRequestedEvent>(OnCreditsPressed);
        EventBus.Subscribe<ExitRequestedEvent>(OnExitPressed);
        EventBus.Subscribe<BackRequestedEvent>(OnBackPressed);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe<PlayRequestedEvent>(OnPlayPressed);
        EventBus.Unsubscribe<SettingsRequestedEvent>(OnSettingsPressed);
        EventBus.Unsubscribe<CreditsRequestedEvent>(OnCreditsPressed);
        EventBus.Unsubscribe<ExitRequestedEvent>(OnExitPressed);
        EventBus.Unsubscribe<BackRequestedEvent>(OnBackPressed);
    }

    private void OnPlayPressed(PlayRequestedEvent playRequestedEvent) => Debug.Log("Play!");

    private void OnSettingsPressed(SettingsRequestedEvent settingsRequestedEvent) => ShowOnly(settingsPanel);

    private void OnCreditsPressed(CreditsRequestedEvent creditsRequestedEvent) => ShowOnly(creditsPanel);

    private void OnExitPressed(ExitRequestedEvent exitRequestedEvent) => Debug.Log("Exit!");

    private void OnBackPressed(BackRequestedEvent backRequestedEvent) => ShowOnly(mainPanel);

    private void ShowOnly(UIPanel target)
    {
        mainPanel.Hide();
        settingsPanel.Hide();
        creditsPanel.Hide();
        target.Show();
    }
}