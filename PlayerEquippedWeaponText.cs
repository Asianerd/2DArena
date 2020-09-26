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
        GetComponentInChildren<Text>().text = Player.GetComponent<PlayerGeneral>().CurrentWeapon.WeaponName;
    }
}
