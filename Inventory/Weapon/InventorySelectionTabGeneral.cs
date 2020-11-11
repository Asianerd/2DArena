using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelectionTabGeneral : MonoBehaviour
{
    public GameObject Name;
    public GameObject Damage;
    public GameObject Mana;
    public GameObject Rarity;
    public GameObject Durability;
    public Button EquipButton;

    public GameObject WeaponSprite;
    public GameObject WeaponType;
    public GameObject RarityBG;
    public static WeaponData.Weapon SelectedWeapon;
    public static bool WeaponIsCurrent;

    public static bool Equippable;

    public GameObject SelectionOutline;
    public GameObject LastButton;

    public Sprite InvisibleImage;
    // on weapon selection tab

    void Update()
    {
        Check();
        if (LastButton != null)
        {
            if (!SelectionOutline.activeSelf) SelectionOutline.SetActive(true);
            SelectionOutline.transform.position = LastButton.transform.position;
        }
        else
        {
            SelectionOutline.SetActive(false);
        }
    }

    public void Set(WeaponData.Weapon weapon, GameObject LButton)
    {
        SelectedWeapon = weapon;
        LastButton = LButton;
        EquipButton.gameObject.SetActive(true);
        // Text Objects
        Name.GetComponent<Text>().text = weapon.WeaponName;
        Damage.GetComponent<Text>().text = $"{weapon.DamageMin * weapon.Level} - {weapon.DamageMax * weapon.Level}";
        Mana.GetComponent<Text>().text = weapon.ManaUsage.ToString();
        Rarity.GetComponent<Text>().text = WeaponData.GlobalWeaponRarityNames[weapon.Rarity];
        if (weapon.IsBreakable) Durability.GetComponent<Text>().text = $"{weapon.Durability}/{weapon.MaxDurability}";
        else Durability.GetComponent<Text>().text = "";

        // Sprite Objects
        switch (weapon.Category)
        {
            case 1:
                WeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalRangeWeaponSpriteList[weapon.WeaponID];
                break;
            case 2:
                WeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.WeaponID];
                break;
            default:
                WeaponSprite.GetComponent<Image>().sprite = WeaponData.GlobalMeleeWeaponSpriteList[weapon.WeaponID];
                break;
        }
        WeaponType.GetComponent<Image>().sprite = WeaponData.GlobalWeaponTypeSprite[weapon.Category];
        WeaponSprite.GetComponent<Image>().SetNativeSize();
        RarityBG.GetComponent<Image>().sprite = WeaponData.GlobalRarityBackground[weapon.Rarity];
    }

    public void Check()
    {
        if(EquipButton.gameObject.activeSelf)
        {
            if (SelectedWeapon != null)
            {
                // Equippable check
                Equippable = ((SelectedWeapon.Durability <= 0) && SelectedWeapon.Durability != -100) ? false : true;
                // For when durability <= 0
                if (!Equippable)
                {
                    EquipButton.interactable = false;
                }
                else
                {
                    EquipButton.interactable = true;
                }
            }

            // For when weapon is already equipped
            if (PlayerGeneral.CurrentWeaponReference == SelectedWeapon)
            {
                WeaponIsCurrent = true;
                EquipButton.GetComponentInChildren<Text>().text = "Unequip";
            }
            else
            {
                WeaponIsCurrent = false;
                EquipButton.GetComponentInChildren<Text>().text = "Equip";
            }
        }
    }

    public void SetEmpty()
    {
        // Text Objects
        Name.GetComponent<Text>().text = "";
        Damage.GetComponent<Text>().text = "";
        Mana.GetComponent<Text>().text = "";
        Rarity.GetComponent<Text>().text = "";
        Durability.GetComponent<Text>().text = "";

        // Button
        EquipButton.gameObject.SetActive(false);

        // Sprite Objects
        WeaponSprite.GetComponent<Image>().sprite = InvisibleImage;
        WeaponType.GetComponent<Image>().sprite = InvisibleImage;
        //WeaponSprite.GetComponent<Image>().SetNativeSize();
        Debug.Log(WeaponData.GlobalRarityBackground.Count());
        RarityBG.GetComponent<Image>().sprite = WeaponData.GlobalRarityBackground[6];
    }
}
