using UnityEngine;

namespace QuestSystem
{
    public class QuestCollectReporter : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private string itemId;

        public void ReportCollected(int amount = 1)
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.ItemCollected, itemId, amount));
        }
    }
}