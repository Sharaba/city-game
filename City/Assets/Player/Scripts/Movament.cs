using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Movament : MonoBehaviour
{


    public bool IsInVeliche = false;
    public bool IsVelicheTrigger = false;
    public float WalkSpeed = .5f;
    public float RunSpeed = 1f;
    public GameObject Player;
    public BuyingCar buyingCar;
    public GameObject CarCamera;
    public Text carText;
    Transform ada;
    public GameObject Car;


    public float TsmoothnessTime = .2f;
    float TurnVelocity;

    public float SpeedSmoothness = .1f;
    float SpeedVelocity;
    public float CurrentSpeed;
    Animator animator;
    Transform CameraRot;
    public Camera camera;
    public bool running = false;
    public StealMode stealMode;
    public AI aI;
    Vector3 firstposition;
    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        CameraRot = camera.transform;
    }
    void Update()
    {
        if (!IsInVeliche)
        {
            if (Vector3.Distance(Player.transform.position, Car.transform.position) < 0.5)
            {
                IsVelicheTrigger = true;
                carText.gameObject.SetActive(true);
            }
            else
            {
                IsVelicheTrigger = false;
                carText.gameObject.SetActive(false);
            }
        }
        if (!IsInVeliche)
        {
            if (!stealMode.IsStealing)
            {
                CarCamera.SetActive(false);
                Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                Vector2 Direction = input.normalized;
                if (Direction != Vector2.zero)
                {
                    float targetRotation = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg + CameraRot.eulerAngles.y;
                    transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref TurnVelocity, TsmoothnessTime);
                }
                //if (input.x != 0 || input.y != 0)
                //    running = Input.GetKey(KeyCode.LeftShift);
                running = Input.GetKey(KeyCode.LeftShift);
                if (input.x == 0 || input.y == 0)
                    running = false;
                float targetSpeed;
                if (running)
                    targetSpeed = RunSpeed;
                else
                    targetSpeed = WalkSpeed;
                CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, targetSpeed, ref SpeedVelocity, TsmoothnessTime);
                if (input.x != 0 || input.y != 0)
                {
                    transform.Translate(transform.forward * (CurrentSpeed) * Time.deltaTime, Space.World);
                }
                float animationSpeed = ((running) ? 1 : .5f) * Direction.magnitude;
                animator.SetFloat("Forward", animationSpeed, TsmoothnessTime, Time.deltaTime);


                if (input.x == 0 && input.y == 0)
                {
                    animator.SetFloat("Forward", 0f);
                }

                    if (animator.GetFloat("Forward") > 0 && animator.GetFloat("Forward") <= 0.6f)
                    {
                             Debug.Log("WALKKKIAAAA AMISDEDASHEVECIII");
                             FindObjectOfType<AudioManager>().Play("Run");
                    }
                else if (animator.GetFloat("Forward") > 0.6f && animator.GetFloat("Forward") < 1f)
                {

                    Debug.Log("RUNIAAA AMISDEDASHEVECIII");
                    FindObjectOfType<AudioManager>().Play("Walk");
                    
                }
                else if (animator.GetFloat("Forward") == 0)
                {
                    Debug.Log("IDLEAAAA AMISDEDASHEVECIIII");
                    FindObjectOfType<AudioManager>().Play("Walk");
                    FindObjectOfType<AudioManager>().Play("Run");
                }
            }

            if (IsVelicheTrigger)
            {
                if (Input.GetKey("e"))
                {
                    if (buyingCar.hasbought)
                    {
                        Player.transform.parent = Car.transform;
                        IsInVeliche = true;
                        Player.SetActive(false);
                        carText.gameObject.SetActive(false);
                        CarCamera.SetActive(true);
                    }
                }

            }

        }
    }
}