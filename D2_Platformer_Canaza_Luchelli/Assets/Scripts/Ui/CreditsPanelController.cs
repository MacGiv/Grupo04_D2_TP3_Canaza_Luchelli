using UnityEngine;
using UnityEngine.UI;

public class CreditsPanelController : UIPanel
{
    [Header("Button References")]
    [SerializeField] private Button backButton;

    protected override void Awake()
    {
        base.Awake();

        backButton.onClick.AddListener(HandleBackClicked);
    }

    private void HandleBackClicked() => EventBus.Publish(new BackRequestedEvent());
}