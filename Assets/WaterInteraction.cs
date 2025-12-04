using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourWater : MonoBehaviour
{
	public GameObject water;
    

    private void Update()
    {
        if (KibbleScript.time > 0)
        {
            KibbleScript.time -= 1;
        }
    }
	private void OnTriggerStay(Collider other)
    {
		if (other.CompareTag("Sink") && KibbleScript.time <= 0)
        {
            if(!water.activeSelf)
            {
                water.SetActive(true);
                KibbleScript.time += 2000;
            }
        }
    }
}
