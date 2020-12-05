using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsGeneral : MonoBehaviour
{
    public Slider fpsSlider;
    public float fps;
    public Button vSyncButton;
    public Sprite toggledButton, untoggledButton;

    private void OnEnable()
    {
        fpsSlider.value = Convert.ToSingle(Application.targetFrameRate);
    }

    private void Update()
    {
        if (fpsSlider.interactable)
        {
            fps = fpsSlider.value;
        }
        FPSSet();
        ChangeVSyncButtonSprite();
    }

    void FPSSet()
    {
        Application.targetFrameRate = Convert.ToInt32(fps);
    }

    void ChangeVSyncButtonSprite()
    {
        vSyncButton.image.sprite = QualitySettings.vSyncCount != 0 ? toggledButton : untoggledButton;
    }

    public void ToggleVSync()
    {
        QualitySettings.vSyncCount = QualitySettings.vSyncCount == 0 ? 1 : 0;
        fpsSlider.interactable = QualitySettings.vSyncCount == 0;
    }
}
