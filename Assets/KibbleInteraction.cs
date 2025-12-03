using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject kibble;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KittyMix"))
        {
            if(!kibble.activeSelf)
            {
                kibble.SetActive(true);
            }    
        }   
    }
}
