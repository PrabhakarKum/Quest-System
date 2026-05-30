using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class QuestNodeDefinition
    {
        public string nodeId;
        public string title;
        [TextArea] public string description;
        public QuestNodeType nodeType;

        [Header("Objective Matching")]
        public string targetId;
        public int requiredAmount = 1;

        [Header("Flow")]
        public bool isTerminalNode;
        public string nextNodeId;
        public List<QuestBranchDefinition> branches = new();

        [Header("Visibility")]
        public bool revealInQuestUI = true;
    }
}