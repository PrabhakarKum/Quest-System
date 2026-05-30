using System;

namespace QuestSystem
{
    [Serializable]
    public class QuestNodeRuntimeState
    {
        public string nodeId;
        public int currentAmount;
        public bool isCompleted;
        public bool isRevealed;
    }
}