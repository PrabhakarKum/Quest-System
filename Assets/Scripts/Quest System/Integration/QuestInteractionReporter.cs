using UnityEngine;

namespace QuestSystem
{
    public class QuestInteractionReporter : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private string interactionId;

        public void ReportInteraction(string interactionValue = null)
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.InteractionPerformed, interactionId, 1, interactionValue));
        }
    }
}