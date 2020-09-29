using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButtonGeneral : MonoBehaviour
{
    public GameObject Player;
    public GameObject Parent;
    public PlayerGeneral.Weapon Weapon;

    public void SetEquippedWeapon()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerGeneral>().CurrentWeapon = Weapon;
    }
}
