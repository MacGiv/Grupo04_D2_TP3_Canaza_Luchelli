using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialDataSo tutorialData;
    private bool hasBeenTriggered = false;
    [SerializeField] private bool triggerOnlyOnce = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerOnlyOnce && hasBeenTriggered) return;

        if (collision.CompareTag("Player"))
        {
            if (tutorialData != null)
            {
                EventBus.Publish(new TutorialEvent(tutorialData, true));
                hasBeenTriggered = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (tutorialData != null)
            {
                EventBus.Publish(new TutorialEvent(tutorialData, false));
            }
        }
    }
}