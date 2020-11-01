using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuGeneral : MonoBehaviour
{
    public GameObject Credits;
    public GameObject Help;
    public GameObject Start;
    public GameObject LoadingScreen;
    public Scene MainScene;

    public void Awake()
    {
        Credits = GameObject.Find("CreditScreen");
        Help = GameObject.Find("HelpScreen");
        Start = GameObject.Find("StartScreen");
        Credits.SetActive(false);
        Help.SetActive(false);
        Start.SetActive(true);
        LoadingScreen.SetActive(false);
    }

    public void Normal()
    {
        Credits.SetActive(false);
        Help.SetActive(false);
        Start.SetActive(true);
        LoadingScreen.SetActive(false);
    }

    public void OpenHelp()
    {
        Start.SetActive(false);
        Help.SetActive(true);
    }

    public void OpenCredits()
    {
        Start.SetActive(false);
        Credits.SetActive(true);
    }

    public void StartGame()
    {
        Start.SetActive(false);
        Credits.SetActive(false);
        Help.SetActive(false);
        LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Main");
    }

    public void StartGame1366x768()
    {
        Start.SetActive(false);
        Credits.SetActive(false);
        Help.SetActive(false);
        LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Main 1366x768");
    }
}
