using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Text text;
    public void play()
    {
        SceneManager.LoadScene("City");
    }
    public void exit()
    {
        Application.Quit();
    }
}
