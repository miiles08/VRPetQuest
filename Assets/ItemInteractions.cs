using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    public GameObject litter;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BoxOfLitter"))
        {
            if (!litter.activeSelf)
            {
                litter.SetActive(true);
            }
        }
    }
}