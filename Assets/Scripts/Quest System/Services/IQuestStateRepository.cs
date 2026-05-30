namespace QuestSystem
{
    public interface IQuestStateRepository
    {
        void Save(QuestJournalState state);
        QuestJournalState Load();
    }
}