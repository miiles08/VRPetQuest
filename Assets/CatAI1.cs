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
    public Transform waitingSpot;
    public Transform[] wanderPoints;


    public GameObject water2;
    public GameObject kibble2; // Allows kibble to SetActive False. Transform follows position, this is the game objcet.


    [Header("Settings")]
    public float arrivalDistance = 0.3f;
    public float bathroomInterval = 30f;
    public float drinkingTimer = 0f;
    public float bowlHeight;



    private float bathroomTimer;
    private enum CatState { Sleeping, Eating, Drinking, Laying, UsingLitter }



    private void Start()
    {
        bathroomTimer = bathroomInterval;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {   
        if (water.gameObject.activeInHierarchy && water.position.y <= bowlHeight)
        {   
            //Calls Water Function
            GoToWater();
            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                     new Vector3(water.transform.position.x, 0, water.transform.position.z)) <= 0.5f)
            {
                // Cat is within 0.5 meters horizontally of the water
                anim.SetTrigger("Drinking");
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("CatSimple_EatDrink_end"))
                {   
                    water2.SetActive(false);
                    Happiness.happiness += 10;
                    anim.SetBool("IsWalking", false);
                    anim.ResetTrigger("Drinking");
                    // Calls GoLayDown Method
                    Transform target = GoLayDown();


                    // Checks distance between agent and wanderpoints
                    //float distance = Vector3.Distance(agent.transform.position, target.position);
                    //if (distance <= 0.5f)
                    //{
                    //    anim.SetBool("Laying", true);

                    //    if (water2.activeSelf || kibble2.activeSelf)
                    //    {   
                    //        anim.SetBool("Laying", false);
                    //        anim.SetTrigger("SomethingToDo");
                    //    }
                    //}
                    //


                }
            }
        }
        if (kibble.gameObject.activeInHierarchy)
        {
            GoToKibble();

            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                     new Vector3(kibble.transform.position.x, 0, kibble.transform.position.z)) <= 0.5f)
            {
                // Cat is within 0.5 meters horizontally of the water
                anim.SetTrigger("Drinking");
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("CatSimple_EatDrink_end"))
                {
                    anim.ResetTrigger("Drinking");
                    kibble2.SetActive(false);
                    Happiness.happiness += 10;
                    anim.SetBool("IsWalking", false);
                    anim.ResetTrigger("Drinking");
                    // Calls GoLayDown function
                    Transform target = GoLayDown();
                    // Checks distance between agent and wanderpoints
                    float distance = Vector3.Distance(agent.transform.position, target.position);
                    if (distance <= 0.5f)
                    {
                        anim.SetBool("Laying", true);

                        if (water2.activeSelf || kibble2.activeSelf)
                        {   
                            anim.SetBool("Laying", false);
                            anim.SetTrigger("SomethingToDo");
                        }
                     }
                     //


                }
            }
        }
        
    }

    // Function for cat to walk towards the water bowl once its full

    private void GoToWater()
    {   
        anim.SetBool("IsWalking", true);

        agent.isStopped = false;

        // Smooth horizontal rotation toward water
        Vector3 lookPos = water.position - transform.position;
        lookPos.y = 0; // ignore vertical differences
        if (lookPos.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // Set the agent destination
        agent.SetDestination(GameObject.FindGameObjectWithTag("WaterBowl").transform.position);

    }

    // Function for cat to walk towards the food bowl once its full

    private void GoToKibble()
    {   
        anim.SetBool("IsWalking", true);

        agent.isStopped = false;

        // Smooth horizontal rotation toward 
        Vector3 lookPos = kibble.position - transform.position;
        lookPos.y = 0; // ignore vertical differences
        if (lookPos.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // Set the agent destination
        agent.SetDestination(GameObject.FindGameObjectWithTag("FoodBowl").transform.position);
    }

    private Transform GoLayDown()
    {
        int index = Random.Range(0,wanderPoints.Length);
        Transform layspot = wanderPoints[index];

        anim.SetBool("IsWalkingSit", true);
        agent.isStopped = false;

        // Smooth horizontal rotation toward 
        Vector3 lookPos = layspot.position - transform.position;
        lookPos.y = 0; // ignore vertical differences
        if (lookPos.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // Set the agent destination
        agent.SetDestination(layspot.position);
        return layspot;
        
        
    }

}
