using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelectionGeneral : MonoBehaviour
{
    public GameObject Name;
    public GameObject Damage;
    public GameObject Mana;
    public GameObject Rarity;
    public GameObject Durability;

    public GameObject Weapon;
    public GameObject WeaponType;
    public GameObject RarityBG;
    public static WeaponData.Weapon SelectedWeapon;

    public GameObject SelectionOutline;
    public GameObject LastButton;

    public static bool Equippable;
    // on weapon selection tab

    void Update()
    {
        if(InventoryGeneral.GamePaused)
        {
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
    }

    public void Set(WeaponData.Weapon weapon,GameObject LButton)
    {
        SelectedWeapon = weapon;
        LastButton = LButton;
        // Text Objects
        Name.GetComponent<Text>().text = weapon.WeaponName;
        Damage.GetComponent<Text>().text = $"{weapon.DamageMin} - {weapon.DamageMax}";
        Mana.GetComponent<Text>().text = weapon.ManaUsage.ToString();
        Rarity.GetComponent<Text>().text = WeaponData.GlobalWeaponRarityNames[weapon.Rarity];
        if (weapon.IsBreakable) Durability.GetComponent<Text>().text = $"{weapon.Durability}/{weapon.MaxDurability}";
        else Durability.GetComponent<Text>().text = "";

        // Sprite Objects
        switch (weapon.Category)
        {
            case 1:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalRangeWeaponSpriteList[weapon.WeaponID];
                break;
            case 2:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.WeaponID];
                break;
            default:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalMeleeWeaponSpriteList[weapon.WeaponID];
                break;
        }
        WeaponType.GetComponent<Image>().sprite = WeaponData.GlobalWeaponTypeSprite[weapon.Category];
        Weapon.GetComponent<Image>().SetNativeSize();
        RarityBG.GetComponent<Image>().sprite = WeaponData.GlobalRarityBackground[weapon.Rarity];

        // Equippable check
        Equippable = ((weapon.Durability <= 0) && weapon.Durability != -100) ? false : true;
        
    }
    
    public void SelectionDefault()
    {
        // Sets to the current weapon; used when the inventory is open (no weapon is selected)
        WeaponData.Weapon weapon = PlayerGeneral.CurrentWeaponReference;
        // Text Objects
        Name.GetComponent<Text>().text = weapon.WeaponName;
        Damage.GetComponent<Text>().text = $"{weapon.DamageMin} - {weapon.DamageMax}";
        Mana.GetComponent<Text>().text = weapon.ManaUsage.ToString();
        Rarity.GetComponent<Text>().text = WeaponData.GlobalWeaponRarityNames[weapon.Rarity];
        if (weapon.IsBreakable) Durability.GetComponent<Text>().text = $"{weapon.Durability}/{weapon.MaxDurability}";
        else Durability.GetComponent<Text>().text = "";

        // Sprite Objects
        switch (weapon.Category)
        {
            case 1:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalRangeWeaponSpriteList[weapon.WeaponID];
                break;
            case 2:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.WeaponID];
                break;
            default:
                Weapon.GetComponent<Image>().sprite = WeaponData.GlobalMeleeWeaponSpriteList[weapon.WeaponID];
                break;
        }
        WeaponType.GetComponent<Image>().sprite = WeaponData.GlobalWeaponTypeSprite[weapon.Category];
        Weapon.GetComponent<Image>().SetNativeSize();
        RarityBG.GetComponent<Image>().sprite = WeaponData.GlobalRarityBackground[weapon.Rarity];
    }
}
