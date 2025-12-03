using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CatAI1 : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator anim;

    [Header("Targets")]
    public Transform kibble;
    public Transform water;
    public Transform litter;
    public Transform laySpot;
    public Transform waitingSpot;

    [Header("Settings")]
    public float arrivalDistance = 0.3f;
    public float bathroomInterval = 30f;

    private float bathroomTimer;
    private enum CatState { Sleeping, Eating, Drinking, Laying, UsingLitter }


    private void Start()
    {
        bathroomTimer = bathroomInterval;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {   
        if (water.gameObject.activeInHierarchy)
        {
            transform.LookAt(water);
            agent.SetDestination(GameObject.FindGameObjectWithTag("WaterBowl").transform.position);
            anim.SetTrigger("Drinking");
        }
    }
}
