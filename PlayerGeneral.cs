using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerGeneral : MonoBehaviour
{
    public GameObject ItemList;
    public Canvas InvCanvas;
    public GameObject RuntimeScript;
    public GameObject DamageBubblePrefab;

    public Camera PlayerCamera;





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

    public WeaponData.Weapon CurrentWeapon;

    public int WeaponCooldown = 0;

    public void ResetCurrentWeapon()
    {
        CurrentWeapon = new WeaponData.Weapon("Fists", 50, 100, 1, 10, 0, 0, 0, 5);
    }

    void Awake()
    {
        ResetCurrentWeapon();
        PlayerCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {

        //double angle = Math.Atan2(Input.mousePosition.y-transform.position.y, Input.mousePosition.x-transform.position.x);
        //Vector3 mousecoords = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);

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
        if ((WeaponCooldown == 0) && Input.GetMouseButton(0))
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

                //Instantiate(DamageBubblePrefab, EnemyPosition, Quaternion.identity).GetComponent<FXDamageBubbleGeneral>().Damage = DamageInflicted;
                //obj.GetComponent<FXDamageBubbleGeneral>().Damage = DamageInflicted;
                WeaponCooldown = CurrentWeapon.WeaponCooldown;
            }
        }
    }
    void RangeAttack()
    {
        float length = 1.4f;
        Vector3 MousePos = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);
        double angle = Math.Atan2(MousePos.y - transform.position.y, MousePos.x - transform.position.x);
        GameObject obj = Instantiate(CurrentWeapon.RangeProjectile, new Vector2(Convert.ToSingle((Math.Cos(angle)*length)+transform.position.x), Convert.ToSingle((Math.Sin(angle)*length)+transform.position.y)), Quaternion.identity);
        obj.GetComponent<GenericRangeProjectile>().Set(angle, CurrentWeapon.SpawnedProjectileSpeed, CurrentWeapon.ProjectileShelfLife,CurrentWeapon);
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
