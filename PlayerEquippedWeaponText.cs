using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquippedWeaponText : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        string WeaponText;
        WeaponText = $"{Player.GetComponent<PlayerGeneral>().CurrentWeapon.WeaponName} {Player.GetComponent<PlayerGeneral>().CurrentWeapon.DamageMin}/{Player.GetComponent<PlayerGeneral>().CurrentWeapon.DamageMax}";
        GetComponentInChildren<Text>().text = WeaponText;
    }
}
