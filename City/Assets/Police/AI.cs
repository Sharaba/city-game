using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    public float radius = 6f;
    public bool invest;
    public Animator animator;
    Vector3 firstposition;
    public bool wentin;
    public Transform target;
    public bool lastseen;
    public bool seen;
    public float angleToPlayer;
    public NavMeshAgent agent;
    public Transform patrollingthing;
    public Vector3 Moving;
    public bool isincoroutine;
    public bool YouveBeenCaught;
    public StealMode player;
    Vector3 direction;
    Vector3 playerspositions;
    public bool helper = false;
    public bool firsttime = false;
    public Vector3 playerspositionhelp=Vector3.zero;
    public float timer;
    public bool istalkingtopedestrian = false;
    public float timerrr = 10f;
    public BattleMode BattleModes;
    void Start()
    {
        animator.GetComponent<Animator>();
        firstposition = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    void Update()
    {
        player = GameObject.Find("Player").GetComponent<StealMode>();
        direction = player.transform.position - transform.position;
        playerspositions = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        var pedestrians = GameObject.FindGameObjectsWithTag("Pedestrian");
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        if (istalkingtopedestrian)
        {
            foreach(var item in pedestrians)
            {
                if (Vector3.Distance(item.transform.position, transform.position) < 0.3)
                {
                    Vector3 directionbetween = item.transform.position - transform.position;
                    transform.LookAt(playerspositions);
                }
            }
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetFloat("Blend", 0);
        }
        if (helper)
        {
            helperr();
            return;
        }
        if (isincoroutine)
            StartCoroutine(LastSeen());
        if (!invest && !lastseen && !seen&&!isincoroutine)
            Move();
        if (!YouveBeenCaught&&!seen&&!isincoroutine)
            Investigate();
    }
    Vector3 playerposition;
    public void Move()
    {
        animator.SetFloat("Blend", 0.5f);
        Moving.x = patrollingthing.position.x;
        Moving.y = transform.position.y;
        Moving.z = patrollingthing.position.z;
        firstposition.y = agent.transform.position.y;
        if (transform.position != Moving && wentin == true)
        {
            Vector3 direction = Moving - transform.position;
            direction.y = 0;
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            agent.SetDestination(Moving);
        }
        if (Vector3.Distance(agent.transform.position, Moving) < 0.5)
        {
            Vector3 direction = firstposition - transform.position;
            direction.y = 0;
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            wentin = false;
            agent.SetDestination(firstposition);
        }
        if (agent.transform.position != firstposition && wentin == false)
        {
            Vector3 direction = firstposition - transform.position;
            direction.y = 0;
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            agent.SetDestination(firstposition);
        }
        if (Vector3.Distance(agent.transform.position, firstposition) < 0.5)
        {
            wentin = true;
        }
    }
 
    public IEnumerator LastSeen()
    {
        isincoroutine = true;
        if (!firsttime)
        {
            agent.ResetPath();
            firsttime = true;
        }
        agent.SetDestination(playerspositions);
        if (Vector3.Distance(agent.transform.position,playerspositions)<0.3) 
        {
            YouveBeenCaught = true;
            seen = true;
            yield return new WaitForSecondsRealtime(0.01f);
            firsttime = false;
            agent.ResetPath();
            seen = false;
            lastseen = false;
            isincoroutine = false;
            YouveBeenCaught = false;
            lastseen = false;
            seen = false;
            agent.speed = 0.2f;
        }
    }
    public void Investigate()
    {
        angleToPlayer = Vector3.Angle(direction, transform.forward);
        if (Vector3.Distance(transform.position, player.transform.position) < 2 && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            if (player.IsRuiningSomething || player.IsStealing || BattleModes.ispunching)
            {
                if (!YouveBeenCaught)
                {
                    agent.speed = 0.4f;
                    animator.SetFloat("Blend", 1);
                    agent.SetDestination(playerspositions);
                    invest = true;
                    seen = true;
                    if (Vector3.Distance(playerspositions, transform.position) < 0.3)
                    {
                        agent.ResetPath();
                        direction.y = 0;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                        YouveBeenCaught = true;
                        StartCoroutine(HasbeenCaught());
                    }
                }
            }
        }
        if(seen)
        {
            invest = false;
            if (!invest)
                StartCoroutine(LastSeen());
        }
    }
    public void helperr()
    {
        agent.ResetPath();
        agent.speed = 0.4f;
        animator.SetFloat("Blend", 1f);
        agent.SetDestination(playerspositions);
        Debug.Log(Vector3.Distance(playerspositions, transform.position));
        if (Vector3.Distance(playerspositions, transform.position) < 0.3)
        {
            agent.ResetPath();
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            YouveBeenCaught = true;
            agent.isStopped = true;
            helper = false;
            StartCoroutine(HasbeenCaught());
        }
    }
    IEnumerator HasbeenCaught()
    {   
        lastseen = false;
        seen = false;
        invest = false;
        agent.speed = 0.2f;
        yield return new WaitForSeconds(2f);
        YouveBeenCaught = false;
        agent.isStopped = false;
        Move();
    }
}