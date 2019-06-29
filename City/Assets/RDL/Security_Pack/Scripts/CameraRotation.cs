using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour {

    public float rotate_amount;
    public Transform target;
   public StealMode stealMode;
    public bool IsAlredyRotated;
    

	// Use this for initialization
	void Start ()
    {
        
      
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (stealMode.IsRuined && !IsAlredyRotated)
        {
            transform.Rotate(0, 70, 0);
            IsAlredyRotated = true;
        }

        if (stealMode.IsRuined == false && IsAlredyRotated)
        {
            transform.Rotate(0, -70, 0);
            IsAlredyRotated = false;
        }
    }
}
