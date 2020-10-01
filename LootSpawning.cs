using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LootSpawning : MonoBehaviour
{
    public Sprite[] WeaponSpriteList = { };
    public GameObject WeaponLoot;
    public SpriteRenderer Renderer;

    public List<PlayerGeneral.Weapon> WeaponList = new List<PlayerGeneral.Weapon>();
    public string[] WeaponNameArray = { "Copper shortsword", "Tin shortsword", "Iron shortsword", "Spear", "Amethyst staff", "Topaz staff", "Sapphire staff", "Amber staff", "Lead shortsword", "Silver shortsword", "Tungsten shortsword", "Iron broadsword", "Lead broadsword", "Silver broadsword", "Tungsten broadsword", "Emerald staff", "Ruby staff", "Crimson shortsword", "Gold shortsword", "Crimson broadsword", "Gold broadsword", "Trident", "Glaive", "Platinum shortsword", "Katana", "Platinum broadsword", "Diamond staff", "Last prism", "Star Wrath", "Phantasm", "Meowmere", "Celebration MK.2", "Solar Eruption", "Holy Water", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test" };

    public void Awake()
    {
        for (int i = 0; i < 56; i++)
        {
            WeaponList.Add(new PlayerGeneral.Weapon(WeaponNameArray[i], 10f, 10f,1f,10f,10));
        }

    }
    public void SpawnWeaponLoot(float XPos,float YPos,PlayerGeneral.Weapon SelectedWeapon,int SelectedWeaponID)
    {
        WeaponLoot.GetComponent<SpriteRenderer>().sprite = WeaponSpriteList[SelectedWeaponID];
        GameObject obj = Instantiate(WeaponLoot,new Vector2(XPos,YPos),Quaternion.identity);
        obj.GetComponent<LootGeneral>().SetWeapon(SelectedWeapon);
    }
}
