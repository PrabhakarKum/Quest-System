using System.Collections.Generic;

namespace QuestSystem
{
    public static class QuestDefinitionExtensions
    {
        public static Dictionary<string, QuestNodeDefinition> BuildNodeMap(this QuestDefinitionSO quest)
        {
            var map = new Dictionary<string, QuestNodeDefinition>();

            if (quest == null || quest.nodes == null)
                return map;

            foreach (var node in quest.nodes)
            {
                if (node != null && !string.IsNullOrWhiteSpace(node.nodeId))
                    map[node.nodeId] = node;
            }

            return map;
        }
    }
}