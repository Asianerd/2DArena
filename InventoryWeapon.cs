using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeapon : MonoBehaviour
{
    public Button ButtonPrefab;
    public GameObject ItemList;
    public GameObject Player;


    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
    }

    public void AddWeapon(string WeaponName)
    {
        ButtonPrefab.GetComponentInChildren<Text>().text = WeaponName;
        Instantiate(ButtonPrefab, ItemList.transform);
    }
}
