using UnityEngine;

namespace QuestSystem
{
    public class QuestRewardInstaller : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private MonoBehaviour[] rewardHandlerBehaviours;

        private void Awake()
        {
            foreach (var behaviour in rewardHandlerBehaviours)
            {
                if (behaviour is IQuestRewardHandler handler)
                {
                    questManager.RegisterRewardHandler(handler);
                }
            }
        }
    }
}