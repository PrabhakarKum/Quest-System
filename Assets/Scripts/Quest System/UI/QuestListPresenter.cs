using UnityEngine;

namespace QuestSystem
{
    public class QuestListPresenter : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private QuestListItemView itemPrefab;
        [SerializeField] private QuestDetailsPresenter detailsPresenter;

        private void OnEnable()
        {
            questManager.OnQuestAccepted += Refresh;
            questManager.OnQuestTracked += Refresh;
            questManager.OnQuestUpdated += Refresh;
            questManager.OnQuestCompleted += Refresh;
            questManager.OnQuestFailed += Refresh;

            Refresh(null);
        }

        private void OnDisable()
        {
            questManager.OnQuestAccepted -= Refresh;
            questManager.OnQuestTracked -= Refresh;
            questManager.OnQuestUpdated -= Refresh;
            questManager.OnQuestCompleted -= Refresh;
            questManager.OnQuestFailed -= Refresh;
        }

        private void Refresh(QuestRuntimeState _)
        {
            for (int i = contentRoot.childCount - 1; i >= 0; i--)
                Destroy(contentRoot.GetChild(i).gameObject);

            foreach (var questState in questManager.GetAcceptedQuests())
            {
                if (questState.status == QuestStatus.Completed || questState.status == QuestStatus.Failed)
                    continue;

                var definition = questManager.GetQuestDefinition(questState.questId);
                var item = Instantiate(itemPrefab, contentRoot);
                item.Bind(questState.questId, definition.questTitle, questState.isTracked, SelectQuest);
            }

            var trackedQuest = questManager.GetTrackedQuest();
            
            detailsPresenter.Show(trackedQuest is { status: QuestStatus.Accepted } ? trackedQuest.questId : null);
        }

        private void SelectQuest(string questId)
        {
            questManager.TrackQuest(questId);
            detailsPresenter.Show(questId);
        }
    }
}