using System;
using System.Collections.Generic;

namespace QuestSystem
{
    [Serializable]
    public class QuestJournalState
    {
        public List<QuestRuntimeState> acceptedQuests = new();
        public string trackedQuestId;

        public QuestRuntimeState GetQuestState(string questId)
        {
            return acceptedQuests.Find(q => q.questId == questId);
        }
    }
}