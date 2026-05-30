namespace QuestSystem
{
    public enum QuestStatus
    {
        Locked,
        Available,
        Accepted,
        Completed,
        Failed
    }

    public enum QuestNodeType
    {
        TalkToNpc,
        CollectItem,
        ReachLocation,
        Interact,
        Custom
    }

    public enum QuestBranchConditionType
    {
        DialogueChoice,
        GameplayOutcome,
        Auto
    }

    public enum QuestRewardType
    {
        GrantXp,
        GrantItem,
        PlayCutscene,
        UnlockQuest,
        UnlockLevel,
        Custom
    }

    public enum QuestEventType
    {
        NpcTalked,
        ItemCollected,
        LocationReached,
        InteractionPerformed,
        GameplayOutcome
    }
}