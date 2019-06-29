using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATMSTEAL : MonoBehaviour
{

    public InputField inputField;
    public Text ExeptionText;
    
    public GameObject StealAtmTable;
    public float TimeTillExit = 2f;
    

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        inputField.gameObject.SetActive(true);

        if (inputField.text == "Player.IsStealing == true" || inputField.text == "player.isstealing==true" || inputField.text == "Player.IsStealing==true" || inputField.text == "player.isstealing == true") 
        {
            TimeTillExit -= Time.deltaTime;
            ExeptionText.text = "Code Ran Successfully";
            if (TimeTillExit <= 0)
            {
                StealAtmTable.gameObject.SetActive(false);
                inputField.gameObject.SetActive(false);
                inputField.text = "";
            }      
        }
    }
}
