using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AINearStealingPlaces : MonoBehaviour
{
    public StealMode player;
    public Vector3 direction;
    public float angleToThingie;
    public NavMeshAgent agent;
    public bool IsFixed = true;
    RuiningThings things;
    Vector3 firstposition;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        firstposition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Vector3.Distance(firstposition,agent.transform.position)<0.2)
        {
            Debug.Log("FF");
            anim.SetBool("IsWalking", false);
        }
        else
            anim.SetBool("IsWalking", true);
        CheckIfNear();
    }
    void CheckIfNear()
    { 
        var objectswithruiningthings = GameObject.FindGameObjectsWithTag("RuiningThing");
        foreach (var item in objectswithruiningthings)
        {
            direction = item.transform.position - transform.position;
            angleToThingie = Vector3.Angle(direction, transform.forward);
            if (Vector3.Distance(transform.position, item.transform.position) < 4 && angleToThingie >= -140 && angleToThingie <= 140)
            {
                foreach (var items in player.ruinin)
                {
                    if (items.Name == item.name)
                    {
                        Debug.Log("Found");
                        if (items.IsRuined)
                        {
                            StartCoroutine(Movetothingie(items.transform.position));
                            IsFixed = false;
                        }
                        things = items;
                    }
                }
            }
        }
    }
    IEnumerator Movetothingie(Vector3 position)
    {
        agent.SetDestination(position);
        if (Vector3.Distance(agent.transform.position, position) < 0.4)
        {
            agent.isStopped = true;
            Debug.Log("YAY");
            anim.SetBool("IsWalking", false);
            yield return new WaitForSeconds(10f);
            agent.isStopped = false;
            anim.SetBool("IsWalking", true);
            player.IsRuined = false;
            IsFixed = true;
            agent.SetDestination(firstposition);
            StopCoroutine(Movetothingie(position));
        }
    }
}
