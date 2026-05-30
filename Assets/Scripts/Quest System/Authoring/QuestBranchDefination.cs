using System;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class QuestBranchDefinition
    {
        public string branchId;
        public QuestBranchConditionType conditionType;
        public string conditionKey;
        public string conditionValue;
        public string nextNodeId;
        public bool failQuestIfMatched;
    }
}