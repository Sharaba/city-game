using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealText : MonoBehaviour
{

    StealMode stealMode;
   
    
    void Start()
    {
        stealMode = gameObject.GetComponent<StealMode>();
    }

    void Update()
    {
        
    }
}
