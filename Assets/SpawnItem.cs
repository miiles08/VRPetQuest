using UnityEngine;
using UnityEngine.EventSystems; // Needed for IPointerClickHandler
using TMPro;
using UnityEngine.SceneManagement;

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

    private void Update()
    {
        totalText.text = "Species: Cat\nHappiness:" + Happiness.happiness.ToString("0.00") + "\n$$$ Spent:" + GlobalTotals.totalSpent.ToString("0.00"+
        "\n\nChores:\nx Food Bowl Emptied: " + GlobalTotals.kBowlFilled + "\n x Water Bowl Emptied: " + GlobalTotals.wBowlFilled + "\n x Litter Box Emptied: " + GlobalTotals.litterBoxFilled);


        if ((Happiness.happiness) > 150 && (GlobalTotals.kBowlFilled + 1 > 3) && (GlobalTotals.wBowlFilled + 1 > 3) && (GlobalTotals.litterBoxFilled + 1 > 3))
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void BuyItem(float price)
    {   
        GlobalTotals.totalSpent += price;              // Add price to total
        
    }

    
    

}

