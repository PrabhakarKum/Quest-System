using UnityEngine;

namespace QuestSystem
{
    public class UnlockQuestRewardHandler : MonoBehaviour, IQuestRewardHandler
    {
        public QuestRewardType RewardType => QuestRewardType.UnlockQuest;

        public void ApplyReward(QuestRewardDefinition reward)
        {
            Debug.Log($"Unlock Quest: {reward.stringValue}");
        }
    }
}