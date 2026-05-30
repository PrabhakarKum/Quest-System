using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuestSystem
{
    public class QuestListItemView : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private GameObject trackedIndicator;

        private string _questId;
        private System.Action<string> _onClicked;

        public void Bind(string questId, string title, bool isTracked, System.Action<string> onClicked)
        {
            _questId = questId;
            _onClicked = onClicked;

            titleText.text = title;
            trackedIndicator.SetActive(isTracked);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => _onClicked?.Invoke(_questId));
        }
    }
}