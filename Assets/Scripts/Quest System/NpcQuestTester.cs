using UnityEngine;
using QuestSystem;

public class NpcQuestTester : MonoBehaviour
{
    [SerializeField] private NpcQuestGiver questGiver;
    [SerializeField] private DialogueQuestBridge dialogueBridge;
    [SerializeField] private string npcId = "npc_healer_01";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool accepted = questGiver.TryGiveQuest();
            Debug.Log("Quest accepted: " + accepted);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            dialogueBridge.ReportNpcTalk(npcId);
            Debug.Log("Reported NPC talk.");
        } 
    }
}