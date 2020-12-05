using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuGeneral : MonoBehaviour
{
    public GameObject credits;
    public GameObject help;
    public GameObject start;
    public GameObject loadingScreen;

    public void Awake()
    {
        credits = GameObject.Find("CreditScreen");
        help = GameObject.Find("HelpScreen");
        start = GameObject.Find("StartScreen");
        credits.SetActive(false);
        help.SetActive(false);
        start.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void Normal()
    {
        credits.SetActive(false);
        help.SetActive(false);
        start.SetActive(true);
        loadingScreen.SetActive(false);
    }

    public void OpenHelp()
    {
        start.SetActive(false);
        help.SetActive(true);
    }

    public void OpenCredits()
    {
        start.SetActive(false);
        credits.SetActive(true);
    }

    public void StartGame()
    {
        start.SetActive(false);
        credits.SetActive(false);
        help.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Main");
    }

    public void StartGame1366x768()
    {
        start.SetActive(false);
        credits.SetActive(false);
        help.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync("Main 1366x768");
    }
}
