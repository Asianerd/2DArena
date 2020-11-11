using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquippedWeaponText : MonoBehaviour
{
    public GameObject Player;
    WeaponData.Weapon Weapon;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        string WeaponText;
        Weapon = PlayerGeneral.CurrentWeaponReference;
        WeaponText = $"{Player.GetComponent<PlayerGeneral>().CurrentWeapon.WeaponName} {Weapon.DamageMin*Weapon.Level}/{Weapon.DamageMax*Weapon.Level}";
        GetComponentInChildren<Text>().text = WeaponText;
    }
}
