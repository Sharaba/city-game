using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PedestrianAI : MonoBehaviour
{
    public NavMeshAgent agent;
    float wanderRadius=15f;
    float wanderTimer = 10f;
    private Transform target;
    public  float timer;
    Vector3 newPos;
    Animator animator;
    public bool firsttime = true;
    Vector3 lastposition;
    Vector3 nowpos;
    Vector3 playerspositions;
    StealMode player;
    Vector3 direction;
    public bool YouveBeenCaught;
    public float angleToPlayer;
    public AI police;
    public Vector3 directionpolice;
    public Vector3 policeposition;
    public bool ifseenstealing=false;
    public float timerforthingie = 4f;
    public float timerforthottie;
    CapsuleCollider CapsuleCollider;
    void Start()
    {
        animator=GetComponent<Animator>();
        //animator.SetBool("IsWalking", true);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    void Update()
    {
        if(animator.GetBool("IsFalling"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            agent.isStopped = true;
            Debug.Log("IM IN");
            StartCoroutine(gettingup());
            return;
        }
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        player = GameObject.Find("Player").GetComponent<StealMode>();
        direction = player.transform.position - transform.position;
        policeposition = new Vector3(police.transform.position.x, 0, police.transform.position.z);
        directionpolice = policeposition - transform.position;
        if (player)
        {
            playerspositions = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        }
        ifstealing();
        if (ifseenstealing)
        { 
            agent.speed = 0.3f;
            agent.SetDestination(policeposition);
            animator.SetFloat("Blend", 1);
            Debug.Log("IsGoingToPolice");
            if(Vector3.Distance(policeposition,transform.position)<0.5)
            {
                transform.LookAt(policeposition);
                animator.SetFloat("Blend", 0);
                police.istalkingtopedestrian = true;
                if (timerforthottie >= timerforthingie)
                {
                    agent.isStopped = true;
                    ifseenstealing = false;
                    police.helper = true;
                    police.istalkingtopedestrian = false;
                    timerforthottie = 0f;
                }
                timerforthottie += Time.deltaTime;
            }
            return;
        }
        agent.speed = 0.2f;
        agent.isStopped = false;
        nowpos = transform.position;
        if (agent.velocity==Vector3.zero) 
        {
            animator.SetFloat("Blend", 0);
        }
        else
            animator.SetFloat("Blend",0.5f);
        lastposition = transform.position;
        timer += Time.deltaTime;
       
        if (firsttime)
        {
            newPos = RandomNavSphere(agent.transform.position, wanderRadius, 3);
            agent.destination = newPos;
            firsttime = false;
        }
        if (timer >= wanderTimer)
        {
            newPos = RandomNavSphere(agent.transform.position, wanderRadius, 3);
            agent.destination = newPos;
            timer = 0;
        }
    }
    void ifstealing()
    {
        Debug.Log("BBB");
        angleToPlayer = Vector3.Angle(direction, transform.forward);
        if (Vector3.Distance(transform.position, player.transform.position) < 2 && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            if (player.IsRuiningSomething || player.IsStealing)
            {
                Debug.Log("FFF");
                ifseenstealing = true;
                
            }
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection=Random.onUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, Random.Range(0f, distance), layermask);
        return navHit.position;
    }
    IEnumerator gettingup()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("IsFalling", false);        
        animator.SetBool("IsGettingUp", true);
        StartCoroutine(gotup());
    }
    IEnumerator gotup()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("IsGettingUp", false);     
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        animator.gameObject.GetComponent<CapsuleCollider>().height = 3;
    }
}
