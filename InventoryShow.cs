using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShow : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject InventoryUI;
    private void Start()
    {
        InventoryUI.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Time.timeScale = 1f;
                GamePaused = false;
                InventoryUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0f;
                GamePaused = true;
                InventoryUI.SetActive(true);
            }
        }
    }
}
