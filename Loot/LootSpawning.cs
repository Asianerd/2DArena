using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LootSpawning : MonoBehaviour
{
    public GameObject weaponLoot;
    public GameObject runtime;

    public List<WeaponData.Weapon> weaponList = new List<WeaponData.Weapon>();

    public void SpawnWeaponLoot(Vector3 SelectedVector3, WeaponData.Weapon SelectedWeapon)
    {
        GameObject obj = Instantiate(weaponLoot, SelectedVector3, Quaternion.identity);
        switch (SelectedWeapon.category)
        {
            case 1:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.globalRangeWeaponSpriteList[SelectedWeapon.weaponID]);
                break;
            case 2:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.globalProjectileSpriteList[SelectedWeapon.weaponID]);
                break;
            default:
                obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon, WeaponData.globalMeleeWeaponSpriteList[SelectedWeapon.weaponID]);
                break;
        }
    }
}
