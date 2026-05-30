using System;

namespace QuestSystem
{
    [Serializable]
    public class QuestEventPayload
    {
        public QuestEventType eventType;
        public string targetId;
        public int amount;
        public string stringValue;

        public QuestEventPayload() { }

        public QuestEventPayload(QuestEventType eventType, string targetId, int amount = 1, string stringValue = null)
        {
            this.eventType = eventType;
            this.targetId = targetId;
            this.amount = amount;
            this.stringValue = stringValue;
        }
    }
}