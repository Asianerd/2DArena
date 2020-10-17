using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuGeneral : MonoBehaviour
{
    public GameObject Credits;
    public GameObject Help;
    public GameObject Start;

    public void Awake()
    {
        Credits = GameObject.Find("CreditScreen");
        Help = GameObject.Find("HelpScreen");
        Start = GameObject.Find("StartScreen");
        Credits.SetActive(false);
        Help.SetActive(false);
        Start.SetActive(true);
    }

    public void Normal()
    {
        Credits.SetActive(false);
        Help.SetActive(false);
        Start.SetActive(true);
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
}
