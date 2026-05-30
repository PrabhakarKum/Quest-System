using UnityEngine;

namespace QuestSystem
{
    public class QuestLocationReporter : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private string locationId;

        public void ReportReached()
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.LocationReached, locationId));
        }
    }
}