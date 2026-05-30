using UnityEngine;

namespace QuestSystem
{
    public class GrantItemRewardHandler : MonoBehaviour, IQuestRewardHandler
    {
        public QuestRewardType RewardType => QuestRewardType.GrantItem;

        public void ApplyReward(QuestRewardDefinition reward)
        {
            Debug.Log($"Grant Item: {reward.stringValue} x{reward.intValue}");
        }
    }
}