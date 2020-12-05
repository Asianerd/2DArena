using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquippedWeaponText : MonoBehaviour
{
    public GameObject player;
    WeaponData.Weapon weapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        string WeaponText;
        weapon = PlayerGeneral.currentWeaponReference;
        WeaponText = $"{player.GetComponent<PlayerGeneral>().currentWeapon.weaponName} {weapon.damageMin*weapon.level}/{weapon.damageMax*weapon.level}";
        GetComponentInChildren<Text>().text = WeaponText;
    }
}
