using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeapon : MonoBehaviour
{
    public Button[] ButtonList;
    void Awake()
    {
        ButtonList = FindObjectsOfType<Button>();
    }

    void Update()
    {
        ButtonList = FindObjectsOfType<Button>();
        Debug.Log(ButtonList.Length);
        Debug.Log(ButtonList);
        for (int i = 0;i<ButtonList.Length;i++)
        {
            Debug.Log(i);
            Debug.Log(ButtonList[i].GetComponentInChildren<Text>().text);
            ButtonList[i].GetComponentInChildren<Text>().text = i.ToString();
        }
    }
}
