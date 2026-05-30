using System;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class QuestRewardDefinition
    {
        public QuestRewardType rewardType;
        public string stringValue;
        public int intValue;
        public bool boolValue;
        public ScriptableObject customPayload;
    }
}