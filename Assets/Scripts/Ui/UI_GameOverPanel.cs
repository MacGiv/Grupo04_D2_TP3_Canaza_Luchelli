using UnityEngine;
using TMPro;

/// <summary>
/// Listens for the GameOverEvent to display the end screen and handle scene navigation.
/// </summary>
public class UIGameOverPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI titleText; 

    private void OnEnable()
    {
        EventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
        Time.timeScale = 1f;
    }

    private void Start()
    {
        panel.SetActive(false);
    }

    private void OnGameOver(GameOverEvent eventData)
    {
        panel.SetActive(true);

        Time.timeScale = 0f;

        if (eventData.IsVictory)
        {
            titleText.text = "¡VICTOLY!";
            titleText.color = Color.green;
        }
        else
        {
            titleText.text = "YOU DIED.";
            titleText.color = Color.red;
        }
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        ServiceLocator.GetService<CustomSceneManager>().RestartGameplay();
    }

    public void OnMainMenuButtonClicked()
    {
        Time.timeScale = 1f;
        ServiceLocator.GetService<CustomSceneManager>().GoToMainMenuFromGameplay();
    }
}