using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinedScript : MonoBehaviour
{
    public StealMode stealMode;
    void Start()
    {
        
    }
    void Update()
    {
        if (stealMode.IsRuined)
        {
            transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
        if (stealMode.IsRuined == false)
        {
            transform.Rotate(0, 0,0);
        }
    }
}
