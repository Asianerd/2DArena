using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeapon : MonoBehaviour
{
    //TO BE REMOVED
    public Button ButtonPrefab;
    public GameObject ItemList;
    public GameObject Player;
    public GameObject Runtime;


    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    public void AddWeapon(WeaponData.Weapon SelectedWeapon)
    {
        ButtonPrefab.GetComponentInChildren<Text>().text = SelectedWeapon.WeaponName;
        var obj = Instantiate(ButtonPrefab, ItemList.transform);
        obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(SelectedWeapon);
        switch(SelectedWeapon.Category)
        {
            case 1: 
                obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().RangeWeaponSpriteList[SelectedWeapon.WeaponID];
                break;
            case 2:
                obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().ProjectileSpriteList[SelectedWeapon.WeaponID];
                break;
            default:
                obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().MeleeWeaponSpriteList[SelectedWeapon.WeaponID];
                break;
        }
    }
}
