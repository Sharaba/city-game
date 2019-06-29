using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Company : MonoBehaviour
{
    public float CompanyBalance = 0f;
    public TextMesh CompanyBalancetext;
    public bool HasPlayerPickedMoney=false;
    public bool bul;
    public Text ActivateCompanyText;
    public bool HasNotBoughtYet;
    public bool HasBeenBought;
   
    public StealMode balance;
    public bool ifstaying = false;
    private void Start()
    {
        HasPlayerPickedMoney = false;
        HasNotBoughtYet = true;
    }
    void Update()
    {
        if (HasNotBoughtYet && ifstaying)
        {
            ActivateCompanyText.gameObject.SetActive(true);

            if (Input.GetKey("space"))
            {
                HasBeenBought = true;
                HasNotBoughtYet = false;
            }
        }


        
        if (HasBeenBought)
        {
            if (!ifstaying)
            {
                CompanyBalance += Time.deltaTime / 2;
            }
            CompanyBalancetext.text = ((int)CompanyBalance).ToString();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && !bul)
        {
            bul = true;
            if (!HasPlayerPickedMoney&&!ifstaying)
            {
                balance.Balance += (int)CompanyBalance;
                CompanyBalance = 0;
            }
            HasPlayerPickedMoney = true;
        }
    }
        
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            bul = false;
            HasPlayerPickedMoney = false;
            ifstaying = false;
            HasNotBoughtYet = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.name=="Player")
        {
            CompanyBalance = 0;
            ifstaying = true;
            ActivateCompanyText.gameObject.SetActive(false);


        }

        
    }
}
