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

    public class Weapon
    {
        public string WeaponName;
        public float DamageMin, DamageMax;
        public Weapon(string name,float DmgRangeMin, float DmgRangeMax)
        {
            WeaponName = name;
            DamageMin = DmgRangeMin;
            DamageMax = DmgRangeMax;
        }
    }

    public List<Weapon> WeaponList = new List<Weapon>();
    public string[] WeaponNameArray = { "Copper shortsword", "Tin shortsword", "Iron shortsword", "Spear", "Amethyst staff", "Topaz staff", "Sapphire staff", "Amber staff", "Lead shortsword", "Silver shortsword", "Tungsten shortsword", "Iron broadsword", "Lead broadsword", "Silver broadsword", "Tungsten broadsword", "Emerald staff", "Ruby staff", "Crimson shortsword", "Gold shortsword", "Crimson broadsword", "Gold broadsword", "Trident", "Glaive", "Platinum shortsword", "Katana", "Platinum broadsword", "Diamond staff", "Last prism", "Star Wrath", "Phantasm", "Meowmere", "Celebration MK.2", "Solar Eruption", "Holy Water", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test" };

    void Awake()
    {
        Renderer = WeaponLoot.AddComponent<SpriteRenderer>();
        //Debug.Log("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        for (int i = 0; i < 56; i++)
        {
            //Debug.Log(WeaponNameArray[i]);
            WeaponList.Add(new Weapon(WeaponNameArray[i], 10f, 10f));
        }
        //Debug.Log(WeaponList.Count);
        //Debug.Log(WeaponList);
    }

    void Start()
    {
        /*
        WeaponList.Append(new Weapon("Copper ShortSword", 10f, 10));
        WeaponList.Append(new Weapon("Silver Shortsword", 15f, 15f));
        WeaponList.Append(new Weapon("Gold Shortsword", 20f, 20f));
        for(int i = 0; i < 57;i++)
        {
            WeaponList.Append(new Weapon("Test", 10f, 10f));
        }
        */
    }

    void Update() {}

    public void SpawnWeaponLoot(float XPos,float YPos,int Weapon_Id)
    {
        WeaponLoot.GetComponent<SpriteRenderer>().sprite = WeaponSpriteList[Weapon_Id];
        WeaponLoot.GetComponent<LootGeneral>().WeaponID = Weapon_Id;
        Debug.Log(WeaponList[Weapon_Id].WeaponName);
        WeaponLoot.GetComponent<LootGeneral>().WeaponName = WeaponList[Weapon_Id].WeaponName;

        Instantiate(WeaponLoot,new Vector2(XPos,YPos),Quaternion.identity);
    }
}
