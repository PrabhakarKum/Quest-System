namespace QuestSystem
{
    public class QuestBranchResolver
    {
        public QuestBranchDefinition Resolve(QuestNodeDefinition node, QuestEventPayload payload)
        {
            if (node?.branches == null || node.branches.Count == 0)
                return null;

            foreach (var branch in node.branches)
            {
                if (IsMatch(branch, payload))
                    return branch;
            }

            return null;
        }

        private bool IsMatch(QuestBranchDefinition branch, QuestEventPayload payload)
        {
            if (branch == null) return false;

            return branch.conditionType switch
            {
                QuestBranchConditionType.Auto => true,
                QuestBranchConditionType.DialogueChoice => payload is { eventType: QuestEventType.InteractionPerformed } &&
                                                           payload.stringValue == branch.conditionValue,
                QuestBranchConditionType.GameplayOutcome => payload is { eventType: QuestEventType.GameplayOutcome } &&
                                                            payload.stringValue == branch.conditionValue,
                _ => false
            };
        }
    }
}