using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponButtonGeneral : MonoBehaviour
{
    public GameObject player;
    public GameObject weaponSelect;
    public WeaponData.Weapon weapon;
    public GameObject runtime;

    public GameObject sText;
    public GameObject sWeaponSprite;
    public GameObject sBorderSprite;

    // on InventoryObject(w)




    public void SetSelectedWeapon()
    {
        weaponSelect = GameObject.FindGameObjectWithTag("WeaponSelectionTab");
        weaponSelect.GetComponent<InventorySelectionTabGeneral>().Set(weapon,gameObject);
    }

    public void SetEquippedWeapon()
    {
        // On the Weapon Selection Tab Button (EquipButton)
        if (InventorySelectionTabGeneral.equippable)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if(InventorySelectionTabGeneral.weaponIsCurrentWeapon)
            {
                player.GetComponent<PlayerGeneral>().ResetCurrentWeapon();
            }
            else
                player.GetComponent<PlayerGeneral>().currentWeapon = InventorySelectionTabGeneral.selectedWeapon;
        }
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon)
    {
        weapon = SelectedWeapon;
        player = GameObject.FindGameObjectWithTag("Player");
        runtime = GameObject.FindGameObjectWithTag("RuntimeScript");

        sText.GetComponent<Text>().text = weapon.level.ToString();

        sBorderSprite.GetComponent<Image>().sprite = WeaponData.globalWeaponBorderList[weapon.rarity];

        switch (weapon.category)
        {
            case 1:
                sWeaponSprite.GetComponent<Image>().sprite = WeaponData.globalRangeWeaponSpriteList[weapon.weaponID];
                sWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            case 2:
                sWeaponSprite.GetComponent<Image>().sprite = WeaponData.globalProjectileSpriteList[weapon.projectileSpriteID];
                sWeaponSprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;
            default:
                sWeaponSprite.GetComponent<Image>().sprite = WeaponData.globalMeleeWeaponSpriteList[weapon.weaponID];
                break;
        }
        sWeaponSprite.GetComponent<Image>().SetNativeSize();

        /*WeaponSelect = GameObject.FindGameObjectWithTag("WeaponSelectionTab");
        WeaponSelect.GetComponent<InventorySelectionGeneral>().Check();*/
    }
}
