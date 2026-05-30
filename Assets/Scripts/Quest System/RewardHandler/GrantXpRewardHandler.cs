using UnityEngine;

namespace QuestSystem
{
    public class GrantXpRewardHandler : MonoBehaviour, IQuestRewardHandler
    {
        public QuestRewardType RewardType => QuestRewardType.GrantXp;
        public void ApplyReward(QuestRewardDefinition reward)
        {
            Debug.Log($"Grant XP: {reward.intValue}");
        }
    }
}