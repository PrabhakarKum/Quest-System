using System.IO;
using UnityEngine;

namespace QuestSystem
{
    public class JsonQuestStateRepository : IQuestStateRepository
    {
        private readonly string _filePath;

        public JsonQuestStateRepository(string fileName = "quest_journal.json")
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public void Save(QuestJournalState state)
        {
            string json = JsonUtility.ToJson(state, true);
            File.WriteAllText(_filePath, json);
        }

        public QuestJournalState Load()
        {
            if (!File.Exists(_filePath))
                return new QuestJournalState();

            string json = File.ReadAllText(_filePath);
            return JsonUtility.FromJson<QuestJournalState>(json) ?? new QuestJournalState();
        }
    }
}