using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KibbleScript : MonoBehaviour
{
    public GameObject kibble;
    public static float time = 0f;

    private void Update()
    {
        if (time > 0 )
        {
            time -= 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("KittyMix") && time <= 0)
        {
            if(!kibble.activeSelf)
            {
                kibble.SetActive(true);
                time += 2000;
            }    
        }   
    }
}
