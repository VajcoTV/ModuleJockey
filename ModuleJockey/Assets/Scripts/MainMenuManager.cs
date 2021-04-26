using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject Options;
    public GameObject Audio;
    public GameObject Controls;
    public GameObject Credits;


    public void LoadMap()
    {
        app.sceneloader.UnloadScene("MainMenu");
        app.sceneloader.LoadScene("Map");
       
    }
    public void OnOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);

    }
    public void OnBack()
    {
        MainMenu.SetActive(true);
        Options.SetActive(false);
        Audio.SetActive(false);
        Controls.SetActive(false);
        Credits.SetActive(false);
    }
    public void Exit()
    {
        //quit
    }
    public void OnAudio()
    {
        MainMenu.SetActive(false);
        Options.SetActive(false);
        Audio.SetActive(true);
    }
    public void OnControls()
    {
        MainMenu.SetActive(false);
        Options.SetActive(false);
        Controls.SetActive(true);

    }
    public void OnCredits()
    {
        MainMenu.SetActive(false);
        Options.SetActive(false);
        Credits.SetActive(true);
    }

}
