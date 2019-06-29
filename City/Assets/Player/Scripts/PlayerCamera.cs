using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{


    public bool LockCursor;
    public float mouseSensitivity = 10f;
   

    public Transform target;
    public float DstFromTarget;
    public Vector2 PitchMinMax = new Vector2(-40, 85);

    public float RSmoothness = .12f;
    Vector3 RotationSVelocity;
    Vector3 CurrentRotation;

    float yaw;
    float pitch;

    // Use this for initialization
    void Start()
    {

        if (LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
      
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, PitchMinMax.x, PitchMinMax.y);
        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw), ref RotationSVelocity, RSmoothness);
        transform.eulerAngles = CurrentRotation;
        transform.position = target.position - (transform.forward * DstFromTarget);
    }
}