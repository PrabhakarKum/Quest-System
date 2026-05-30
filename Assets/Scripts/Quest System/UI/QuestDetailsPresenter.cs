using TMPro;
using UnityEngine;

namespace QuestSystem
{
    public class QuestDetailsPresenter : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private TMP_Text questTitleText;
        [SerializeField] private TMP_Text questDescriptionText;
        [SerializeField] private TMP_Text currentObjectiveText;

        public void Show(string questId)
        {
            var quest = questManager.GetQuestDefinition(questId);
            var state = questManager.Journal.GetQuestState(questId);

            if (quest == null || state == null)
            {
                questTitleText.text = "No Active Quest";
                questDescriptionText.text = "";
                currentObjectiveText.text = "";
                return;
            }

            questTitleText.text = quest.questTitle;
            questDescriptionText.text = quest.shortDescription;

            var currentNode = quest.GetNodeById(state.currentNodeId);

            if (currentNode == null)
            {
                currentObjectiveText.text = $"Missing node for id: [{state.currentNodeId}]";
                return;
            }

            currentObjectiveText.text = currentNode.description;
        }
    }
}