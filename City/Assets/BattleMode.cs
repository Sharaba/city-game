using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class BattleMode : MonoBehaviour
{
    public bool pedestriandown=false;
    public class Target
    {
        public string Name;
        public float HP;
        public float DMG;
    }
    public bool IsPunching;
    public Animator animator;
    public Animator[] foranim;
    public Target target;
    public GameObject tt;
    public bool ispunching;
    public List<Target> Targets = new List<Target>() { new Target() { Name = "regina", HP = 100, DMG = 4, },
        new Target() { Name = "malcolm", HP = 100, DMG = 4 },
        new Target() { Name = "regina (1)", HP=100, DMG=4 },
        new Target() { Name = "malcolm (1)", HP = 100, DMG = 4 },
        new Target() { Name = "remy", HP = 100, DMG = 4 },
        new Target() { Name = "remy (2)", HP = 100, DMG = 4 },
        new Target() { Name = "jasper (1)", HP = 100, DMG = 4 },
        new Target() { Name = "jasper (2)", HP = 100, DMG = 4 } };
    void Update()
    {
        if (Input.GetKey("h"))
        {
            animator.SetBool("IsPunching", true);
            ispunching = true;
            CheckTheNearest();
            target.HP -= 100;
            if(target.HP<=0)
            {
                if (target.Name == "regina")
                {
                    foranim[0].SetBool("IsFalling", true);
                    foranim[0].gameObject.GetComponent<CapsuleCollider>().height = 1;
                }
                if (target.Name == "malcolm")
                {
                    foranim[1].SetBool("IsFalling", true);
                    foranim[1].gameObject.GetComponent<CapsuleCollider>().height = 1;
                }
                if (target.Name == "regina (1)")
                {
                    foranim[3].SetBool("IsFalling", true);
                    foranim[3].gameObject.GetComponent<CapsuleCollider>().height = 1;
                }
                if (target.Name == "remy (2)")
                {
                    foranim[4].SetBool("IsFalling", true);
                    foranim[4].gameObject.GetComponent<CapsuleCollider>().height = 1;
                }
                if (target.Name == "malcolm (1)")
                {
                    foranim[2].SetBool("IsFalling", true);
                    foranim[2].gameObject.GetComponent<CapsuleCollider>().height = 1;

                }
                if (target.Name == "remy")
                {
                    foranim[5].SetBool("IsFalling", true);
                    foranim[5].gameObject.GetComponent<CapsuleCollider>().height = 1;
                }
            }
        }
        else
        {
            animator.SetBool("IsPunching", false);
            ispunching = false;
        }
    }
    public void CheckTheNearest()
    {
        var Pedestrians = GameObject.FindGameObjectsWithTag("Pedestrian");
        foreach(var item in Pedestrians)
        {
            if(Vector3.Distance(item.transform.position,transform.position)<0.5)
            {
                foreach(var itemm in Targets)
                {
                    if(item.name==itemm.Name)
                    {
                        Debug.Log(item.name);
                        target = itemm;
                        tt = item;
                    }
                }
            }
        }
    }
}
