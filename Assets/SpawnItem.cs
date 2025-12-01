using UnityEngine;
using UnityEngine.EventSystems; // Needed for IPointerClickHandler
using TMPro;

public class SpawnBoxVR : MonoBehaviour
{   
    public TMP_Text totalText;   // Assign the UI text that shows total
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

    public void BuyItem(float price)
    {
        GlobalTotals.totalSpent += price;              // Add price to total
        UpdateTotalText();                 // Update the UI
    }

    private void UpdateTotalText()
    {
        totalText.text = "Species: Cat\nHappiness:\n$$$ Spent:" + GlobalTotals.totalSpent.ToString("0.00");
    }
}

