using System.Collections.Generic;

namespace QuestSystem
{
    public class QuestRewardProcessor
    {
        private readonly Dictionary<QuestRewardType, IQuestRewardHandler> _handlers = new();

        public void RegisterHandler(IQuestRewardHandler handler)
        {
            if (handler == null) return;
            _handlers[handler.RewardType] = handler;
        }

        public void ApplyRewards(QuestDefinitionSO quest)
        {
            if (quest == null) return;

            foreach (var reward in quest.rewards)
            {
                if (_handlers.TryGetValue(reward.rewardType, out var handler))
                {
                    handler.ApplyReward(reward);
                }
            }
        }
    }
}