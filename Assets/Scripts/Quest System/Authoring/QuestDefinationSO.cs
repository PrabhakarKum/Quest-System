using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest System/Quest Definition", fileName = "QuestDefinition")]
    public class QuestDefinitionSO : ScriptableObject
    {
        [Header("Identity")]
        public string questId;
        public string questTitle;
        [TextArea] public string shortDescription;

        [Header("Quest Start")]
        public string startNpcId;

        [Header("Quest Flow")]
        public string startNodeId;
        public List<QuestNodeDefinition> nodes = new();

        [Header("Dependencies")]
        public List<string> prerequisiteQuestIds = new();

        [Header("Rewards")]
        public List<QuestRewardDefinition> rewards = new();

        public QuestNodeDefinition GetNodeById(string nodeId)
        {
            if (string.IsNullOrWhiteSpace(nodeId))
                return null;

            string safeId = nodeId.Trim();

            return nodes.Find(n => n != null && !string.IsNullOrWhiteSpace(n.nodeId) && n.nodeId.Trim() == safeId);
        }
    }
}