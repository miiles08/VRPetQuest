using UnityEngine;
using UnityEngine.EventSystems; // Needed for IPointerClickHandler

public class SpawnBoxVR : MonoBehaviour
{
    public GameObject boxPrefab;   // Drag your prefab here
    public Transform spawnPoint;   // Drag the GameObject where items should appear

    // This will be called by the UI Button's OnClick event
    public void SpawnItem()
    {
        if (boxPrefab != null && spawnPoint != null)
        {
            Instantiate(boxPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}