using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]

public class UIButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Audio Settings")]
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip hoverClip;

    [Header("Animation Settings")]
    [SerializeField] private float hoverScale = 1.15f;
    [SerializeField] private float scaleSpeed = 20.0f;
    private Vector3 defaultScale;
    private Vector3 targetScale;

    private void Start()
    {
        defaultScale = transform.localScale;
        targetScale = defaultScale;
    }

    private void Update() 
        => transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleSpeed * Time.unscaledDeltaTime);

    public void OnPointerClick(PointerEventData eventData) 
        => EventBus.Publish(new SfxRequestedEvent { clip = clickClip });

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = defaultScale * hoverScale;

        EventBus.Publish(new SfxRequestedEvent { clip = hoverClip });
    }

    public void OnPointerExit(PointerEventData eventData) => targetScale = defaultScale;
}