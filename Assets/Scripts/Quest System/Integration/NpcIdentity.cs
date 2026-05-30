using UnityEngine;

namespace QuestSystem
{
    public class NpcIdentity : MonoBehaviour
    {
        [SerializeField] private string npcId;

        public string NpcId => npcId;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(npcId))
            {
                npcId = gameObject.name.ToLower().Replace(" ", "_");
            }
        }
#endif
    }
}