using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Happiness : MonoBehaviour
{

    public float decreaseInterval = 10f; // seconds
    private float timer = 0f;
    public static float happiness = 75f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5f)
        {
            Happiness.happiness -= 1; 
            timer = 0f;
            Debug.Log("Happiness: " + Happiness.happiness);
        }
    }


}
