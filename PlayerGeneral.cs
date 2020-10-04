using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

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

    // ATK
    public GameObject[] EnemyArray;
    public float PlayerDamage = 3f;

    public WeaponData.Weapon CurrentWeapon = new WeaponData.Weapon("Fists", 50, 100, 1, 10, 0, 0, 0, 5);

    public int WeaponCooldown = 0;

    void Update()
    {

        //double angle = Math.Atan2()


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
        if ((WeaponCooldown == 0) && Input.GetMouseButtonDown(0))
        {
            WeaponCooldown = CurrentWeapon.WeaponCooldown;
            switch (CurrentWeapon.Category)
            {
                case 0:
                    MeleeAttack();
                    break;
                case 1:
                    RangeAttack();
                    break;
                case 2:
                    break;
                default:
                    break;
            }

            /*
            EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject i in EnemyArray)
            {
                Vector3 EnemyPosition = new Vector3(i.transform.position.x, i.transform.position.y);
                if ((i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) < AttackRange) && (Input.GetMouseButtonDown(0)))
                {
                    i.GetComponent<EnemyGeneral>().MinusHealth(PlayerDamage + DamageInflicted, CurrentWeapon.Knockback, transform.position);
                    Instantiate(DamageBubblePrefab, EnemyPosition, Quaternion.identity).GetComponent<FXDamageBubbleGeneral>().Damage = DamageInflicted;
                    WeaponCooldown = CurrentWeapon.WeaponCooldown;
                }
            }*/
        }
        if (WeaponCooldown > 0) WeaponCooldown--;
    }

    void MeleeAttack()
    {
        float DamageInflicted = UnityEngine.Random.Range(CurrentWeapon.DamageMin, CurrentWeapon.DamageMax);
        EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in EnemyArray)
        {
            Vector2 EnemyPosition = new Vector2(i.transform.position.x, i.transform.position.y);
            if (i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) <= CurrentWeapon.AttackRange)
            {
                i.GetComponent<EnemyGeneral>().MinusHealth(PlayerDamage + DamageInflicted, CurrentWeapon.Knockback, transform.position);

                GameObject obj = Instantiate(DamageBubblePrefab, EnemyPosition, Quaternion.identity);
                obj.GetComponent<FXDamageBubbleGeneral>().Damage = DamageInflicted;
                WeaponCooldown = CurrentWeapon.WeaponCooldown;
            }
        }
    }
    void RangeAttack()
    {
        GameObject obj = Instantiate(CurrentWeapon.RangeProjectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        //Debug.Log($"{Convert.ToSingle(Math.Atan2(Input.mousePosition.y-transform.position.y, Input.mousePosition.x-transform.position.x ))}   ::   {(Convert.ToSingle(Math.Atan2(Input.mousePosition.y-transform.position.y, Input.mousePosition.x-transform.position.x)))}");
        double angle = Math.Atan2(Input.mousePosition.y-transform.position.y,Input.mousePosition.x-transform.position.x);

        Debug.Log(angle);
        obj.GetComponent<GenericRangeProjectile>().Set(angle, CurrentWeapon.SpawnedProjectileSpeed, CurrentWeapon.ProjectileShelfLife);
    }
    void ProjectileAttack()
    {
        Instantiate(CurrentWeapon.Projectile);
    }

    public void AppendInventory(WeaponData.Weapon WantedWeapon)
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
