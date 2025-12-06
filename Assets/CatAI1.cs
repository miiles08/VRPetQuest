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
    public GameObject litter2;


    [Header("Settings")]
    public float arrivalDistance = 0.3f;
    public float bathroomInterval = 30f;
    public float drinkingTimer = 0f;
    public float bowlHeight;



    private float bathroomTimer;
    private enum CatState { Sleeping, Eating, Drinking, Laying, UsingLitter }
    private bool layingDown;
    private Transform layingTarget;
    private bool awakeCat;
    private bool catAte;
    private bool ready;
    public static bool ate;

    private void Start()
    {
        bathroomTimer = bathroomInterval;
        layingDown = false;
        awakeCat = true;
        ready = false;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {   
        if ((water2.activeSelf || kibble2.activeSelf || (litter2.activeSelf && catAte == true)) && layingDown == false)
        {   
        anim.SetBool("Laying", false);
        anim.SetTrigger("SomethingToDo");
        anim.SetTrigger("WakeUp");
        }

        //So when the cat goes to the water bowl, it can only move when the idle animation 
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CatSimple_Idle_1"))
        {
            if (catAte == true && litter.gameObject.activeInHierarchy)
            {
                GoToLitter();
            }
            else
            {
                awakeCat = true;
            }

            
        }

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
                    GlobalTotals.wBowlFilled += 1;
                    Happiness.happiness += 25;
                    anim.SetBool("IsWalking", false);
                    anim.ResetTrigger("Drinking");
                    // Calls GoLayDown Method
                    layingTarget = GoLayDown();
                }
            }
        }
        if (kibble.gameObject.activeInHierarchy && awakeCat == true)
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
                    GlobalTotals.kBowlFilled += 1;
                    Happiness.happiness += 25;
                    anim.SetBool("IsWalking", false);
                    anim.ResetTrigger("Drinking");
                    catAte = true;
                    ate = true;

                    // Calls GoLayDown function
                    layingTarget = GoLayDown();
                   
                }
            }
        }
        //Checks distance between agent and wanderpoints
        if (layingDown == true)
        {
            float distance = Vector3.Distance(agent.transform.position, layingTarget.position);
            if (distance <= 0.5f)
                 {
                 anim.SetBool("Laying", true);
                 layingDown = false;
                 anim.ResetTrigger("SomethingToDo");
                 anim.ResetTrigger("WakeUp");


            }
        }
        if (ready == true)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z),
                     new Vector3(litter.transform.position.x, 0, litter.transform.position.z)) <= 0.5f)
                {
                    // Cat is within 0.1 meters horizontally of the litter
                    anim.SetBool("IsWalkingDefecate", true);
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("DonePooping"))
                    {   
                        litter2.SetActive(false);
                        GlobalTotals.litterBoxFilled += 1;
                        catAte = false;
                        Happiness.happiness += 15;
                        anim.SetBool("IsWalking", false);
                        anim.SetBool("IsWalkingDefecate", false);
                        ready = false;
                        // Calls GoLayDown Method
                        layingTarget = GoLayDown();
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

        private void GoToLitter()

        //Cat to be awake, litteractive, catAte == true
    {   
        anim.SetBool("IsWalking", true);

        agent.isStopped = false;

        // Smooth horizontal rotation toward water
        Vector3 lookPos = litter.position - transform.position;
        lookPos.y = 0; // ignore vertical differences
        if (lookPos.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRot = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }

        // Set the agent destination
        agent.SetDestination(GameObject.FindGameObjectWithTag("LitterBox").transform.position);

        ready = true;
        

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

        layingDown = true;
        awakeCat = false;

        return layspot;
        
        
    }


}
