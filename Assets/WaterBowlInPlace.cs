using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowlInPlace : MonoBehaviour
{
    public static bool waterbowlinplace = false;
    public static GameObject water;

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("WaterBowl") && water.gameObject.activeInHierarchy) // make sure the bowl has this tag
    {
        waterbowlinplace = true;
        Debug.Log("Bowl in place!");
    }
}


    
}
