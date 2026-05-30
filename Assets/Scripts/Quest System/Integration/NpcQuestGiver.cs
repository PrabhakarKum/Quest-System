using UnityEngine;

namespace QuestSystem
{
    public class NpcQuestGiver : MonoBehaviour
    {
        [SerializeField] private NpcIdentity npcIdentity;
        [SerializeField] private QuestManager questManager;
        [SerializeField] private string questId;

        public bool TryGiveQuest()
        {
            if (npcIdentity == null || questManager == null)
                return false;

            var quest = questManager.GetQuestDefinition(questId);
            if (quest == null)
                return false;

            if (quest.startNpcId != npcIdentity.NpcId)
                return false;

            return questManager.AcceptQuest(questId, true);
        }
    }
}