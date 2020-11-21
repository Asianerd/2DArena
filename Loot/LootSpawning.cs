using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LootSpawning : MonoBehaviour
{
    public GameObject WeaponLoot;
    public GameObject Runtime;

    public List<WeaponData.Weapon> WeaponList = new List<WeaponData.Weapon>();

    public void SpawnWeaponLoot(Vector3 SelectedVector3, WeaponData.Weapon SelectedWeapon)
    {
        GameObject obj = Instantiate(WeaponLoot, SelectedVector3, Quaternion.identity);
        switch (SelectedWeapon.Category)
        {
            case 1:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.GlobalRangeWeaponSpriteList[SelectedWeapon.WeaponID]);
                break;
            case 2:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.GlobalProjectileSpriteList[SelectedWeapon.WeaponID]);
                break;
            default:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.GlobalMeleeWeaponSpriteList[SelectedWeapon.WeaponID]);
                break;
        }
    }
}
