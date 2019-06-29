using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForLight : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 DirectionalLight = new Vector3(0.01f,0.01f, 0.01f);
        transform.Rotate(DirectionalLight);

        if (transform.rotation.x % 360 > 0)
        {

        }
    }
}
