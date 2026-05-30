namespace QuestSystem
{
    public interface IQuestRewardHandler
    {
        QuestRewardType RewardType { get; }
        void ApplyReward(QuestRewardDefinition reward);
    }
}