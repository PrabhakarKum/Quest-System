using UnityEngine;

namespace QuestSystem
{
    public class PlayCutsceneRewardHandler : MonoBehaviour, IQuestRewardHandler
    {
        public QuestRewardType RewardType => QuestRewardType.PlayCutscene;

        public void ApplyReward(QuestRewardDefinition reward)
        {
            Debug.Log($"Play Cutscene: {reward.stringValue}");
        }
    }
}