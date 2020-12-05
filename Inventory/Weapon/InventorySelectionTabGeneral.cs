using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelectionTabGeneral : MonoBehaviour
{
    public GameObject name;
    public GameObject damage;
    public GameObject mana;
    public GameObject rarity;
    public GameObject durability;
    public Button equipButton;

    public GameObject weaponSprite;
    public GameObject weaponType;
    public GameObject rarityBackground;
    public static WeaponData.Weapon selectedWeapon;
    public static bool weaponIsCurrentWeapon;

    public static bool equippable;

    public GameObject selectionOutline;
    public GameObject lastButton;

    public Sprite invisibleImage;
    // on weapon selection tab

    void Update()
    {
        Check();
        if (lastButton != null)
        {
            if (!selectionOutline.activeSelf) selectionOutline.SetActive(true);
            selectionOutline.transform.position = lastButton.transform.position;
        }
        else
        {
            selectionOutline.SetActive(false);
        }
    }

    public void Set(WeaponData.Weapon weapon, GameObject LButton)
    {
        selectedWeapon = weapon;
        lastButton = LButton;
        equipButton.gameObject.SetActive(true);
        // Text Objects
        name.GetComponent<Text>().text = weapon.weaponName;
        damage.GetComponent<Text>().text = $"{weapon.damageMin * weapon.level} - {weapon.damageMax * weapon.level}";
        mana.GetComponent<Text>().text = weapon.manaUsage.ToString();
        rarity.GetComponent<Text>().text = WeaponData.globalWeaponRarityNames[weapon.rarity];
        if (weapon.isBreakable) durability.GetComponent<Text>().text = $"{weapon.durability}/{weapon.maxDurability}";
        else durability.GetComponent<Text>().text = "";

        // Sprite Objects
        switch (weapon.category)
        {
            case 1:
                weaponSprite.GetComponent<Image>().sprite = WeaponData.globalRangeWeaponSpriteList[weapon.weaponID];
                break;
            case 2:
                weaponSprite.GetComponent<Image>().sprite = WeaponData.globalProjectileSpriteList[weapon.weaponID];
                break;
            default:
                weaponSprite.GetComponent<Image>().sprite = WeaponData.globalMeleeWeaponSpriteList[weapon.weaponID];
                break;
        }
        weaponType.GetComponent<Image>().sprite = WeaponData.globalWeaponTypeSprite[weapon.category];
        weaponSprite.GetComponent<Image>().SetNativeSize();
        rarityBackground.GetComponent<Image>().sprite = WeaponData.globalRarityBackground[weapon.rarity];
    }

    public void Check()
    {
        if(equipButton.gameObject.activeSelf)
        {
            if (selectedWeapon != null)
            {
                // Equippable check
                equippable = ((selectedWeapon.durability <= 0) && selectedWeapon.durability != -100) ? false : true;
                // For when durability <= 0
                equipButton.interactable = equippable;
                /*if (!Equippable)
                {
                    EquipButton.interactable = false;
                }
                else
                {
                    EquipButton.interactable = true;
                }*/
            }

            // For when weapon is already equipped
            if (PlayerGeneral.currentWeaponReference == selectedWeapon)
            {
                weaponIsCurrentWeapon = true;
                equipButton.GetComponentInChildren<Text>().text = "Unequip";
            }
            else
            {
                weaponIsCurrentWeapon = false;
                equipButton.GetComponentInChildren<Text>().text = "Equip";
            }
        }
    }

    public void SetEmpty()
    {
        // Text Objects
        name.GetComponent<Text>().text = "";
        damage.GetComponent<Text>().text = "";
        mana.GetComponent<Text>().text = "";
        rarity.GetComponent<Text>().text = "";
        durability.GetComponent<Text>().text = "";

        // Button
        equipButton.gameObject.SetActive(false);

        // Sprite Objects
        weaponSprite.GetComponent<Image>().sprite = invisibleImage;
        weaponType.GetComponent<Image>().sprite = invisibleImage;
        //WeaponSprite.GetComponent<Image>().SetNativeSize();
        Debug.Log(WeaponData.globalRarityBackground.Count());
        rarityBackground.GetComponent<Image>().sprite = WeaponData.globalRarityBackground[6];
    }
}
