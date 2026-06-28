using UnityEngine;
using UnityEngine.UI;

public class MainPanelController : UIPanel
{
    [Header("Button References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    protected override void Awake()
    {
        base.Awake();

        playButton.onClick.AddListener(HandlePlayClicked);
        settingsButton.onClick.AddListener(HandleSettingsClicked);
        creditsButton.onClick.AddListener(HandleCreditsClicked);
        exitButton.onClick.AddListener(HandleExitClicked);
    }

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(HandlePlayClicked);
        settingsButton.onClick.RemoveListener(HandleSettingsClicked);
        creditsButton.onClick.RemoveListener(HandleCreditsClicked);
        exitButton.onClick.RemoveListener(HandleExitClicked);
    }

    private void HandlePlayClicked() => EventBus.Publish(new PlayRequestedEvent());

    private void HandleSettingsClicked() => EventBus.Publish(new SettingsRequestedEvent());

    private void HandleCreditsClicked() => EventBus.Publish(new CreditsRequestedEvent());

    private void HandleExitClicked() => EventBus.Publish(new ExitRequestedEvent());
}