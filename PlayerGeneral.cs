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

    

    //HP
    public float HP = 100, HPMax = 100, HPRegen = 1;
    public int HPRegenTime = 100, HPRegenClock = 0;
    public List<WeaponData.Weapon> InventoryWeapon = new List<WeaponData.Weapon>();
    public List<ArrayList> InventoryLoot = new List<ArrayList>();
    public List<string> InventoryLootName = new List<string>();
    public WeaponData.Weapon EquippedWeapon;
    public List<WeaponData.Weapon> GlobalWeaponList = new List<WeaponData.Weapon>();

    // ATK
    public GameObject[] EnemyArray;
    public float AttackRange = 1f;
    public float PlayerDamage = 3f;
    public WeaponData.Weapon CurrentWeapon = new WeaponData.Weapon("Fists", 2, 3, 5, 2, 50,0,false,0,0);

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
                i.GetComponent<EnemyGeneral>().MinusHealth(PlayerDamage+DamageInflicted,CurrentWeapon.Knockback,transform.position);
                Instantiate(DamageBubblePrefab, EnemyPosition,Quaternion.identity);
            }
        }
        
    }
    public void AppendInventory(int WeaponType, WeaponData.Weapon WantedWeapon)
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
