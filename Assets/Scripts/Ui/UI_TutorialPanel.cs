using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITutorialPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panelContainer;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image displayImage;

    private Coroutine animationCoroutine;

    private void OnEnable()
    {
        EventBus.Subscribe<TutorialEvent>(OnTutorialToggle);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<TutorialEvent>(OnTutorialToggle);
    }

    private void Start()
    {
        panelContainer.SetActive(false);
    }

    private void OnTutorialToggle(TutorialEvent eventData)
    {
        if (eventData.Show)
        {
            StopAnimation();

            panelContainer.SetActive(true);
            titleText.text = eventData.Data.Title;
            descriptionText.text = eventData.Data.Description;

            if (eventData.Data.AnimationFrames != null && eventData.Data.AnimationFrames.Length > 0)
            {
                displayImage.gameObject.SetActive(true);
                animationCoroutine = StartCoroutine(AnimateTutorialRoutine(eventData.Data));
            }
            else
            {
                displayImage.gameObject.SetActive(false);
            }
        }
        else
        {
            panelContainer.SetActive(false);
            StopAnimation();
        }
    }

    private void StopAnimation()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    /// <summary>
    /// Loops through frames to simulate a GIF overlay inside Unity UI.
    /// </summary>
    private IEnumerator AnimateTutorialRoutine(TutorialDataSo data)
    {
        int currentFrame = 0;
        while (true)
        {
            displayImage.sprite = data.AnimationFrames[currentFrame];
            currentFrame = (currentFrame + 1) % data.AnimationFrames.Length;
            yield return new WaitForSeconds(data.FrameRate);
        }
    }
}