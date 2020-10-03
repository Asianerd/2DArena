using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButtonGeneral : MonoBehaviour
{
    public GameObject Player;
    public GameObject Parent;
    public WeaponData.Weapon Weapon;

    public void SetEquippedWeapon()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerGeneral>().CurrentWeapon = Weapon;
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon)
    {
        Weapon = SelectedWeapon;
    }
}
