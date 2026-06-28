using UnityEngine;

/// <summary>
/// Base class for UI panels that controls visibility via CanvasGroup.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    protected CanvasGroup CanvasGroup { get; private set; }

    protected virtual void Awake() => CanvasGroup = GetComponent<CanvasGroup>();

    /// <summary>
    /// Makes the panel fully visible and interactive.
    /// </summary>
    public virtual void Show()
    {
        CanvasGroup.alpha = 1.0f;
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Hides the panel and disables all interaction.
    /// </summary>
    public virtual void Hide()
    {
        CanvasGroup.alpha = 0.0f;
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;
    }
}