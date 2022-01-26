using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject ControlPanel;
    public void StartButton()
    {
        SceneManager.LoadScene("Game"); //Load level scene
    }

    public void ButtonQuit()
    {
        Application.Quit(); //Exit the game
    }

    public  void Controls() //Open Controls panel
    {
        ControlPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        ControlPanel.SetActive(false);
    }
}
