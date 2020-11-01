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
    public GameObject SBorderSprite;

    // on InventoryObject(w)



    public void SetSelectedWeapon()
    {
        WeaponSelect = GameObject.FindGameObjectWithTag("WeaponSelectionTab");
        WeaponSelect.GetComponent<InventorySelectionGeneral>().Set(Weapon,gameObject);
    }

    public void SetEquippedWeapon()
    {
        if (InventorySelectionGeneral.Equippable)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if(InventorySelectionGeneral.WeaponIsCurrent)
            {
                Player.GetComponent<PlayerGeneral>().ResetCurrentWeapon();
            }
            else
                Player.GetComponent<PlayerGeneral>().CurrentWeapon = InventorySelectionGeneral.SelectedWeapon;
        }
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon)
    {
        Weapon = SelectedWeapon;
        Player = GameObject.FindGameObjectWithTag("Player");
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");

        SText.GetComponent<Text>().text = Weapon.Level.ToString();

        SBorderSprite.GetComponent<Image>().sprite = WeaponData.GlobalWeaponBorderList[Weapon.Rarity];

        switch (Weapon.Category)
        {
            case 1:
                SWeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalRangeWeaponSpriteList[Weapon.WeaponID];
                SWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            case 2:
                SWeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalProjectileSpriteList[Weapon.ProjectileSpriteID];
                SWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            default:
                SWeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalMeleeWeaponSpriteList[Weapon.WeaponID];
                break;
        }
        SWeaponSprite.GetComponent<Image>().SetNativeSize();

        /*WeaponSelect = GameObject.FindGameObjectWithTag("WeaponSelectionTab");
        WeaponSelect.GetComponent<InventorySelectionGeneral>().Check();*/
    }
}
