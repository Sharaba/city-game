using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPicker : MonoBehaviour
{
    public Camera StealCamera;
    public GameObject StealTable;
    public float Timer;
	public float FullTime;
    public float BalanceHolder;
    public Camera PlayerCamera;
    public GameObject Player;
    public StealMode StealModeCont;
    public GameObject[] DollarsFBX;
	public Image TimerIm;
	public Scrollbar TimeBar;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {	RayCastFunc();	}
        TimerFunc();
        ActivationController();
		if (Timer <= 0) {
			StealTable.gameObject.SetActive(false);
            TimeBar.gameObject.SetActive(false);
		}
    }

    void ActivationController()
    {
		
        if (BalanceHolder == 40)
        {
			Timer = FullTime;

            for (int i = 0; i < DollarsFBX.Length; i++)
            {
                DollarsFBX[i].gameObject.SetActive(true);
            }

            BalanceHolder = 0;

            StealTable.gameObject.SetActive(false);
            TimeBar.gameObject.SetActive(false);

            

         
        }
    }

    void TimerFunc()
    {
		TimeBar.gameObject.SetActive(true);
        Timer -= Time.deltaTime;
		TimerIm.fillAmount = Timer / FullTime;
        
    }

    void RayCastFunc()
    {
        RaycastHit hit;
		Ray ray = StealCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform != null)
            {
                if (hit.transform.gameObject.name.Contains("Doll"))
                {
                    BalanceHolder += 10;
                    Debug.Log(hit.transform.gameObject.name);
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}