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


    public void AddWeapon(PlayerGeneral.Weapon SelectedWeapon)
    {
        ButtonPrefab.GetComponent<InventoryWeaponButtonGeneral>().Weapon = SelectedWeapon;
        ButtonPrefab.GetComponentInChildren<Text>().text = SelectedWeapon.WeaponName;
        Instantiate(ButtonPrefab, ItemList.transform);
    }
}
