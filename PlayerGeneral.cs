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
        public float DamageMin, DamageMax, WeaponRange, Knockback;
        public int WeaponCooldown;
        public Weapon(string name, float DmgRangeMin, float DmgRangeMax,float WpnKnockback, float WpnRange, int WpnCooldown)
        {
            WeaponName = name;
            DamageMin = DmgRangeMin;
            DamageMax = DmgRangeMax;
            Knockback = WpnKnockback;
            WeaponRange = WpnRange;
            WeaponCooldown = WpnCooldown;
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
        ItemList = GameObject.FindGameObjectWithTag("InventoryWeaponItemList");
    }


    // ATK
    public GameObject[] EnemyArray;
    public float AttackRange = 1f;
    public float PlayerDamage = 3f;
    public Weapon CurrentWeapon = new Weapon("Fists", 2, 3, 5, 2, 50);

    void Update()
    {
        Regen();
        Attack();
    }
    public void MinusHealth(float losthealth)
    {
        HP -= losthealth;
    }
    
    public void Attack()
    {
        float DamageInflicted = Random.Range(CurrentWeapon.DamageMin, CurrentWeapon.DamageMax);
        EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in EnemyArray)
        {
            if ((i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) < AttackRange) && (Input.GetMouseButtonDown(0)))
            {
                i.GetComponent<EnemyGeneral>().MinusHealth(PlayerDamage + DamageInflicted);
            }
        }
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
