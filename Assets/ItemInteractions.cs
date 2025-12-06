using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject litter;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BoxOfLitter") && (CatAI1.ate == true) && (KibbleScript.time <= 0))
        {
            if (!litter.activeSelf)
            {
                litter.SetActive(true);
                KibbleScript.time += 3000;
            }
        }
    }
}