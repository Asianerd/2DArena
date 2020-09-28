using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButtonGeneral : MonoBehaviour
{
    public GameObject Player;


    public void SetEquippedWeapon()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerGeneral>().CurrentWeapon = new PlayerGeneral.Weapon("New Weapon",1f,10f,10f,10f,100);
    }
}
