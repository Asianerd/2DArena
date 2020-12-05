using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    // Holds data for basically everything
    // Manages most input events too

    //GameObjects
    public GameObject debugScreen;

    public static bool showDebug = false;
    public static int FPS;

    void Update()
    {
        CheckDebug();
        debugScreen.SetActive(showDebug);
    }

    void CheckDebug()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            showDebug = !showDebug;
        }
    }
}
