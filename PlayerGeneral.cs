using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGeneral : MonoBehaviour
{
    public GameObject ItemList;
    public Canvas InvCanvas;
    public GameObject RuntimeScript;
    public GameObject DamageBubblePrefab;

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
    public List<Weapon> InventoryWeapon = new List<Weapon>();
    public List<ArrayList> InventoryLoot;
    public List<string> InventoryLootName;
    public Weapon EquippedWeapon;

    public List<Weapon> GlobalWeaponList = new List<Weapon>();

    public void Awake()
    {
        string[] WeaponNameArray = { "Copper shortsword", "Tin shortsword", "Iron shortsword", "Spear", "Amethyst staff", "Topaz staff", "Sapphire staff", "Amber staff", "Lead shortsword", "Silver shortsword", "Tungsten shortsword", "Iron broadsword", "Lead broadsword", "Silver broadsword", "Tungsten broadsword", "Emerald staff", "Ruby staff", "Crimson shortsword", "Gold shortsword", "Crimson broadsword", "Gold broadsword", "Trident", "Glaive", "Platinum shortsword", "Katana", "Platinum broadsword", "Diamond staff", "Last prism", "Star Wrath", "Phantasm", "Meowmere", "Celebration MK.2", "Solar Eruption", "Holy Water", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test" };
        ItemList = GameObject.FindGameObjectWithTag("InventoryWeaponItemList");
        RuntimeScript = GameObject.FindGameObjectWithTag("RuntimeScript");
        for (int i = 0; i < 56; i++)
        {
            GlobalWeaponList.Add(new Weapon(WeaponNameArray[i], 10f, 10f,10f,10f,10));
        }
        foreach (Weapon x in GlobalWeaponList)
        {
            Debug.Log(x.WeaponName);
        }
    }


    // ATK
    public GameObject[] EnemyArray;
    public float AttackRange = 1f;
    public float PlayerDamage = 3f;
    public Weapon CurrentWeapon = new Weapon("Fists", 2, 3, 5, 2, 50);

    void Update()
    {
        if (!RuntimeScript.GetComponent<InventoryShow>().GamePaused)
        {
            Regen();
            Attack();
        }
    }
    public void MinusHealth(float losthealth)
    {
        HP -= losthealth;
    }
    
    void Attack()
    {
        float DamageInflicted = Random.Range(CurrentWeapon.DamageMin, CurrentWeapon.DamageMax);
        EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in EnemyArray)
        {
            Vector3 EnemyPosition = new Vector3(i.transform.position.x, i.transform.position.y);
            if ((i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) < AttackRange) && (Input.GetMouseButtonDown(0)))
            {
                DamageBubblePrefab.GetComponent<FXDamageBubbleGeneral>().Damage = DamageInflicted;
                i.GetComponent<EnemyGeneral>().MinusHealth(PlayerDamage + DamageInflicted);
                Instantiate(DamageBubblePrefab, EnemyPosition,Quaternion.identity);
            }
        }
        
    }
    public void AppendInventory(int WeaponType, Weapon WantedWeapon)
    {
        InvCanvas.GetComponent<InventoryWeapon>().AddWeapon(WantedWeapon);
        InventoryWeapon.Add(WantedWeapon);
    }
    void Regen()
    {
        if(HPRegenClock == 0)
        {
            if (HP < HPMax)
            {
                if ((HP + HPRegen > HPMax) && (HP < HPMax)) HP = HPMax;
                else HP += HPRegen;
            }
        }
        HPRegenClock++;
        if (HPRegenClock >= HPRegenTime+1) //>= so that HPRegenClock has 100 variants
            HPRegenClock = 0;
    }
}
