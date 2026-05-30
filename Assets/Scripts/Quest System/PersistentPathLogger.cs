using UnityEngine;

public class PersistentPathLogger : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }
}