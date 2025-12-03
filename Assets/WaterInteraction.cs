using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourWater : MonoBehaviour
{
	public GameObject water;

	private void OnTriggerStay(Collider other)
    {
		if (other.CompareTag("Sink"))
        {
            if(!water.activeSelf)
            {
                water.SetActive(true);
            }
        }
    }
}
