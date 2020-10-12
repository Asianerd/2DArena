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
    //public string[] WeaponNameArray = { "Copper shortsword", "Tin shortsword", "Iron shortsword", "Spear", "Amethyst staff", "Topaz staff", "Sapphire staff", "Amber staff", "Lead shortsword", "Silver shortsword", "Tungsten shortsword", "Iron broadsword", "Lead broadsword", "Silver broadsword", "Tungsten broadsword", "Emerald staff", "Ruby staff", "Crimson shortsword", "Gold shortsword", "Crimson broadsword", "Gold broadsword", "Trident", "Glaive", "Platinum shortsword", "Katana", "Platinum broadsword", "Diamond staff", "Last prism", "Star Wrath", "Phantasm", "Meowmere", "Celebration MK.2", "Solar Eruption", "Holy Water", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test" };

    public void SpawnWeaponLoot(Vector3 SelectedVector3, WeaponData.Weapon SelectedWeapon)
    {
        GameObject obj = Instantiate(WeaponLoot, SelectedVector3, Quaternion.identity);
        obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon,Runtime.GetComponent<WeaponData>().MeleeWeaponSpriteList[SelectedWeapon.WeaponID]);
    }
}
