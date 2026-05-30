using System;
using System.Collections.Generic;

namespace QuestSystem
{
    [Serializable]
    public class QuestRuntimeState
    {
        public string questId;
        public QuestStatus status;
        public bool isTracked;
        public string currentNodeId;
        public bool rewardClaimed;
        public bool branchLocked;
        public string chosenBranchId;
        public List<QuestNodeRuntimeState> nodeStates = new();

        public QuestNodeRuntimeState GetNodeState(string nodeId)
        {
            return nodeStates.Find(n => n.nodeId == nodeId);
        }
    }
}