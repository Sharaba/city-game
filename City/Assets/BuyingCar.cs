using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingCar : MonoBehaviour
{
    StealMode BL;
    public GameObject car;
    public bool hasbought = false;
    private void Start()
    {
        BL = gameObject.GetComponent<StealMode>();
    }
    private void Update()
    {
       if(Vector3.Distance(car.transform.position,transform.position)<0.5f)
        {
            if(BL.Balance>=1000  )
            {
                hasbought = true;
                BL.Balance -= 1000;
            }
        }
    }
}
