using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsGeneral : MonoBehaviour
{
    public Slider FPSSlider;
    public float FPS;

    private void Update()
    {
        FPS = FPSSlider.value;
        FPSSet();
        Debug.Log(FPS);
    }

    void FPSSet()
    {
        if (FPS < 10)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 10;
        }
        if (FPS == 0)
        {
            QualitySettings.vSyncCount = 1;
        }
        if(!(FPS < 10) && !(FPS == 0))
        {
            Application.targetFrameRate = Convert.ToInt32(FPS);
        }


    }
}
