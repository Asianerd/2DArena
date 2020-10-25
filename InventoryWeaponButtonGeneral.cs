using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButtonGeneral : MonoBehaviour
{
    public GameObject Player;
    public GameObject WeaponSelect;
    public WeaponData.Weapon Weapon;
    public GameObject Runtime;

    public Button SButton;
    public GameObject SText;
    public GameObject SWeaponSprite;



    public void SetEquippedWeapon()
    {
        Player.GetComponent<PlayerGeneral>().CurrentWeapon = Weapon;
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon)
    {
        Weapon = SelectedWeapon;
        Player = GameObject.FindGameObjectWithTag("Player");
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");

        SText.GetComponent<Text>().text = Weapon.Level.ToString();

        
        switch (Weapon.Category)
        {
            case 1:
                SWeaponSprite.GetComponent<Image>().sprite = Runtime.GetComponent<WeaponData>().RangeWeaponSpriteList[Weapon.WeaponID];
                SWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            case 2:
                SWeaponSprite.GetComponent<Image>().sprite = Runtime.GetComponent<WeaponData>().ProjectileSpriteList[Weapon.ProjectileSpriteID];
                SWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            default:
                SWeaponSprite.GetComponent<Image>().sprite = Runtime.GetComponent<WeaponData>().MeleeWeaponSpriteList[Weapon.WeaponID];
                break;
        }
        SWeaponSprite.GetComponent<Image>().SetNativeSize();
    }
}
