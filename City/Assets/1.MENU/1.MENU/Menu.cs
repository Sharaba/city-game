using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void gameStart()
    {
        SceneManager.LoadScene("City");
    }

    public void gameExit()
    {
        Application.Quit();
    }
}
