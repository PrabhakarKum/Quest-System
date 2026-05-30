using UnityEngine;

namespace QuestSystem
{
    public class UnlockLevelRewardHandler : MonoBehaviour, IQuestRewardHandler
    {
        public QuestRewardType RewardType => QuestRewardType.UnlockLevel;

        public void ApplyReward(QuestRewardDefinition reward)
        {
            Debug.Log($"Unlock Level: {reward.stringValue}");
        }
    }
}