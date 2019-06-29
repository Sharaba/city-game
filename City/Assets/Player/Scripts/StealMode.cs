using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StealMode : MonoBehaviour
{
    public Animator GangController;
    public bool IsAtm1;
	public Image RuinFill;
	public Scrollbar RuinBar;
	public Image StealFill;
	public Scrollbar StealBar;
    public BuyingCar buyingCar;
    public bool IsRuined = false;
    public bool HasBeenSeen = false;
    public bool CanISteal = false;
    public float StealTime = 5;
	public float FullStealTime=5;
    public bool Stealed = false;
    bool IsTextNeeded = false;
    public Text BalanceText;
    public Text WarningText;
    public float Balance = 0;
    bool HasEnteredSafeHouse = false;
    public bool canruin = false;
    public bool IsStealing = false;
    public bool IsRuiningSomething = false;
    float RuiningTime = 5;
	float FullRuiningTime=5;
    public Text TextBalanceText;
    public Text TextWarningText;
    public bool found = false;
    public Animator anim;
    public Movament movament;
    public bool CanPlayerBeTeleported1 = false;
    public bool oncehas;
    public AI aI;
    public GameObject[] StealTable;
    public Rank rank;
    public bool isHotDogs = false;
    Vector3 firstposition;
    public bool IsIceCream = false;
    List<Market> markets = new List<Market>() { new Market(){ Name="HotDogs",FullStealTime=5,StealTime=5f,StealAbleMoney=30f,},
    new Market(){Name="IceCreams",StealTime=7f, FullStealTime = 7f, StealAbleMoney=10f,},
    new Market(){Name="Van",StealTime = 5f, FullStealTime = 5f, StealAbleMoney = 20f,},
    new Market(){Name= "Atm1",StealTime = 10f, FullStealTime = 10f, StealAbleMoney = 30,}};

    public RuiningThings ruinedthing;
    public List<RuiningThings> ruinin = new List<RuiningThings>
    {
        new RuiningThings(){Name="Ads",RuinTime=5f }
    };
    private void Start()
    {
        firstposition = transform.position;
        anim = gameObject.GetComponent<Animator>();
    }
   public Market stealablethingie;
    void Update()
    {
        BalanceText.text = ((int)Balance).ToString();
        if (IsRuined)
        {
            foreach (var item in ruinin)
            {
                if (item.Name == ruinedthing.Name)
                    item.IsRuined = true;
            }
        }
        if (!IsStealing)
            anim.SetBool("IsStealing", false);
        if (CanPlayerBeTeleported1)
        {
            if (Input.GetKey("space"))
            {
                Application.LoadLevel(0);
            }
        }

        if (!IsStealing && !oncehas)
        {
            CheckTheNearest();
			StealBar.gameObject.SetActive(false);
        }
        if (IsStealing)
        {
            FindObjectOfType<AudioManager>().Play("Tension");
            StealBar.gameObject.SetActive(true);
        }
        
        if (!found)
			StealBar.gameObject.SetActive(false);
		RuinBar.gameObject.SetActive(false);
        if (!HasBeenSeen && found)
        {
            if (!Stealed)
            {
                if (Input.GetKey("space"))
                {
                    StartCoroutine(Steal());

                    anim.SetBool("IsStealing", true);
                }
                else
                {
                    IsStealing = false;
                    anim.SetBool("IsStealing", false);
                }
            }
            else
            {
                Debug.Log("YOU HAVE TO GO TO SAFEHOUSE");
             

            }


        }
        if (HasEnteredSafeHouse)
        {
            if (Stealed)
            {
                Balance += stealablethingie.StealAbleMoney;
                BalanceText.text = ((int)Balance).ToString();
                StealTime = 5;
                GangController.SetBool("IsGood", true);
                found = false;
                rank.plusXP = (int)stealablethingie.StealAbleMoney;

                FindObjectOfType<AudioManager>().Stop("Tension");
            }

            else
            {
                GangController.SetBool("IsAngry", true);
            }
            oncehas = false;
            Stealed = false;
            
            StealBar.gameObject.SetActive(false);
        }
        if (HasBeenSeen)
        {
            StealTime = 5;
            WarningText.text = "Camera can see you";
            if (IsStealing || IsRuiningSomething && Balance > 0)
            {
                Balance -= 5;
                BalanceText.text = ((int)Balance).ToString();
            }
        }
        if (!HasBeenSeen)
        {
            WarningText.text = "";
        }
        if (canruin)
        {
            if (Input.GetKey("space"))
            {
                Ruin();
            }
            else
                IsRuiningSomething = false;
        }
        if (aI.YouveBeenCaught)
        {
            Debug.Log("YouveBeenCaught");
            StartCoroutine(ssssss());
            transform.position = new Vector3 { x= -8.087f,y=firstposition.y,z= -152.708f};
            if (Balance > 100)
            {
                Balance -= 100;
            }
            aI.YouveBeenCaught = false;
        }





        if (IsStealing || Stealed || IsStealing && Stealed || CanISteal || IsStealing && Stealed && CanISteal)
        {
        }
    }
    IEnumerator ssssss()
    {
        yield return new WaitForSecondsRealtime(2);
    }
    public void CheckTheNearest()
    {
        var objectswithsteal = GameObject.FindGameObjectsWithTag("StealPlace");
        //var objectswithruiningthings = GameObject.FindGameObjectsWithTag("RuiningThing");
        foreach (var item in objectswithsteal)
        {
            if (Vector3.Distance(transform.position, item.transform.position) < 0.5f)
            {
    
                foreach (var items in markets)
                {
                    if (items.Name == item.name)
                    {
                        stealablethingie = items;
                        StealTime = stealablethingie.StealTime;
                        FullStealTime = stealablethingie.FullStealTime;
                        
                    }
                }
                //CanISteal = true;
            }
            else if (Vector3.Distance(transform.position, item.transform.position) > 0.5f && !found)
            {
                found = false;
                CanISteal = false;
                StealTime = 5;
                RuinBar.gameObject.SetActive(false);
            }
        }
        //foreach (var item in objectswithruiningthings)
        //{ 
        //    if (Vector3.Distance(transform.position, item.transform.position) < 0.5f)
        //    {
        //        foreach (var items in ruinin)
        //        {
        //            if (items.Name == item.name)
        //            {

        //                canruin = true;
        //                ruinedthing = items;
        //            }
        //        }
        //        //CanISteal = true;
        //    }
        //    else if(Vector3.Distance(transform.position,item.transform.position)>0.5&&!canruin)
        //    {
        //        canruin = false;
        //        RuiningTime = 5;
        //        IsRuiningSomething = false;
        //    }
        //}
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Atm1")
        {
            IsAtm1 = true;
        }
        if (other.name == "IceCreams")
          {
              IsIceCream = true;
          }
        if(other.name == "HotDogs")
          {
              isHotDogs = true;
          }
        if (other.tag == "CameraView")
        {
            HasBeenSeen = true;
        }
        if (other.tag == "SafeHouse")
        {
            HasEnteredSafeHouse = true;
        }
        if (other.tag == "StealPlace")
        {
            found = true;
            CanISteal = true;
        }
        if (other.name == "NewSceneFastFoodTrigger")
        {
            CanPlayerBeTeleported1 = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Atm1")
        {
            IsAtm1 = false;
        }
        if (other.name == "IceCreams")
        {
            IsIceCream = false;
        }

        if (other.tag == "CameraView")
        {
            HasBeenSeen = false;
        }
        if (other.tag == "SafeHouse")
        {
            Stealed = false;
            HasEnteredSafeHouse = false;
            GangController.SetBool("IsAngry", false);
            GangController.SetBool("IsGood", false);
        }
        if (other.tag == "StealPlace")
        {
            found = false;
            canruin = false;
            IsStealing = false;
            StealTime = 5;
        }

        if (other.name == "HotDogs")
        {
            isHotDogs = false;
            Debug.Log("FALSEA BOZISHVILIVIYO");
        }
        if (other.name == "NewSceneFastFoodTrigger")
        {
            CanPlayerBeTeleported1 = false;
        }
    }
    IEnumerator Steal()
    {
        if (found == true)
        {
			StealBar.gameObject.SetActive(true);
            StealTime -= Time.deltaTime;
			StealFill.fillAmount = StealTime / FullStealTime;
			IsStealing = true;
            if (StealTime <= 0)
            {
                Stealed = true;
                IsStealing = false;

              

                yield return new WaitForSeconds(0.1f);
                if (Stealed)
                {

                    if (isHotDogs)
                    {
                        StealTable[0].gameObject.SetActive(true);
                        
                    }

                    if (IsIceCream)
                    {
                        StealTable[1].gameObject.SetActive(true);
                    }

                    if (IsAtm1)
                    {
                        StealTable[2].gameObject.SetActive(true);
                    }
                }
                IsRuined = false;
                CanISteal = false;
                oncehas = true;
            }
        }
        else
        {
            IsStealing = false;
        }
    }
    void Ruin()
    {
		RuinBar.gameObject.SetActive(true);
        RuiningTime -= Time.deltaTime;
        IsRuiningSomething = true;

		RuinFill.fillAmount = RuiningTime / FullRuiningTime;
        if (RuiningTime <= 0)
        {   
            IsRuiningSomething = false;
            IsRuined = true;

        }
    }
     
}