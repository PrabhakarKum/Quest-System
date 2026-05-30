using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest Database", fileName = "QuestDatabase")]
    public class QuestDatabaseSo : ScriptableObject
    {
        public List<QuestDefinitionSO> quests = new();

        public QuestDefinitionSO GetQuestById(string questId)
        {
            return quests.Find(q => q.questId == questId);
        }
    }
}