using UnityEngine;

namespace QuestSystem
{
    public class DialogueQuestBridge : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;

        public void ReportNpcTalk(string npcId)
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.NpcTalked, npcId));
        }

        public void ReportDialogueChoice(string npcId, string choiceId)
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.InteractionPerformed, npcId, 1, choiceId));
        }

        public void ReportGameplayOutcome(string outcomeId)
        {
            questManager.HandleEvent(new QuestEventPayload(QuestEventType.GameplayOutcome, outcomeId, 1, outcomeId));
        }
    }
}