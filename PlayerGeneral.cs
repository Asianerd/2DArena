using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGeneral : MonoBehaviour
{
    public GameObject ItemList;
    public Canvas InvCanvas;
    public class Weapon
    {
        public string WeaponName;
        public float DamageMin, DamageMax;
        public Weapon(string name, float DmgRangeMin, float DmgRangeMax)
        {
            WeaponName = name;
            DamageMin = DmgRangeMin;
            DamageMax = DmgRangeMax;
        }
    }

    //HP
    public float HP = 100, HPMax = 100, HPRegen = 1;
    public int HPRegenTime = 100, HPRegenClock = 0;
    public List<int> InventoryWeapon;
    public List<string> InventoryWeaponName;
    public List<ArrayList> InventoryLoot;
    public List<string> InventoryLootName;
    public Weapon EquippedWeapon;

    private void Awake()
    {
        EquippedWeapon = new Weapon("Default", 5f, 10f);
        ItemList = GameObject.FindGameObjectWithTag("InventoryWeaponItemList");
    }

    void Update()
    {
        Regen();
    }
    public void MinusHealth(float losthealth)
    {
        HP -= losthealth;
    }
    public void AppendInventory(int WeaponType,int WeaponID,string WeaponName)
    {
        InvCanvas.GetComponent<InventoryWeapon>().AddWeapon(WeaponName);
        InventoryWeapon.Add(WeaponID);
    }
    void Regen()
    {
        if(HPRegenClock == 0)
        {
            if (HP < HPMax)
            {
                if ((HP + HPRegen > HPMax) && (HP < HPMax))
                {
                    HP = HPMax;
                }
                else
                {
                    HP += HPRegen;
                }
            }
        }
        HPRegenClock++;
        if (HPRegenClock >= HPRegenTime+1) //>= so that HPRegenClock has 100 variants
            HPRegenClock = 0;
    }
}
