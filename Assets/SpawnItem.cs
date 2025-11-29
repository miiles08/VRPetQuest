using UnityEngine;
using UnityEngine.EventSystems; // Needed for IPointerClickHandler

public class SpawnBoxVR : MonoBehaviour, IPointerClickHandler
{
    public GameObject boxPrefab;    // Drag your prefab here
    public Transform playerCamera;  // Drag your VR camera here
    public float spawnDistance = 2f;

    // This method is called automatically by Unity when the Ray Interactor clicks this object
    public void OnPointerClick(PointerEventData eventData)
    {
        if (boxPrefab != null && playerCamera != null)
        {
            Vector3 spawnPos = playerCamera.position + playerCamera.forward * spawnDistance;
            Instantiate(boxPrefab, spawnPos, Quaternion.identity);
        }
    }
}