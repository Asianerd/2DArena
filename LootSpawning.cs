using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public Weapon[] WeaponList;

    void Awake()
    {
        Renderer = WeaponLoot.AddComponent<SpriteRenderer>();
    }

    void Start()
    {
        WeaponList.Append(new Weapon("Copper ShortSword", 10f, 10));
        WeaponList.Append(new Weapon("Silver Shortsword", 15f, 15f));
        WeaponList.Append(new Weapon("Gold Shortsword", 20f, 20f));
    }

    void Update() {}

    public void SpawnWeaponLoot(float XPos,float YPos,int Weapon_Id)
    {
        WeaponLoot.GetComponent<SpriteRenderer>().sprite = WeaponSpriteList[Weapon_Id];
        WeaponLoot.GetComponent<LootGeneral>().WeaponID = Weapon_Id;

        Instantiate(WeaponLoot,new Vector2(XPos,YPos),Quaternion.identity);
    }
}
