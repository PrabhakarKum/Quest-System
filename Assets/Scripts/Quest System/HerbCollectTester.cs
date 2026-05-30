using UnityEngine;
using QuestSystem;

public class HerbCollectTester : MonoBehaviour
{
    [SerializeField] private QuestCollectReporter collectReporter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            collectReporter.ReportCollected(1);
            Debug.Log("Collected 1 herb.");
        }
    }
}