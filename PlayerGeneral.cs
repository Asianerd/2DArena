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
    public static Vector3 MousePosition;
    public static float MouseAngle;
    public static Vector2 PlayerPosition;

    public GameObject WpnObject;
    public float CosmeticWeaponDistance = 0.5f;



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
    public static WeaponData.Weapon CurrentWeaponReference;

    public int WeaponCooldown = 0;

    public void ResetCurrentWeapon()
    {
        CurrentWeapon = new WeaponData.Weapon("Fists", 50, 100, 1, 100, 0, 0, 0, 0, 5, 0.05f);
    }

    void Awake()
    {
        ResetCurrentWeapon();
        PlayerCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {

        MousePosition = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);

        MouseAngle = Mathf.Atan2(MousePosition.y - transform.position.y, MousePosition.x - transform.position.x);

        PlayerPosition = new Vector2(transform.position.x, transform.position.y);

        WpnObject.GetComponent<SpriteRenderer>().sprite = RuntimeScript.GetComponent<WeaponData>().WeaponSpriteList[CurrentWeapon.WeaponID];

        CurrentWeaponReference = CurrentWeapon;

        if (!InventoryShow.GamePaused)
        {
            Regen();
            Attack();
            ShowWeapon();
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
        WpnObject.GetComponent<WeaponObject>().Swing();
    }
    void RangeAttack()
    {
        float length = 1.4f;
        GameObject obj = Instantiate(CurrentWeapon.RangeProjectile, new Vector2(Convert.ToSingle((Mathf.Cos(MouseAngle)*length)+transform.position.x), Convert.ToSingle((Mathf.Sin(MouseAngle)*length)+transform.position.y)), Quaternion.identity);
        obj.GetComponent<GenericRangeProjectile>().Set(MouseAngle,CurrentWeapon);
    }
    void ProjectileAttack()
    {
        Instantiate(CurrentWeapon.Projectile);
    }

    void ShowWeapon()
    {
        if (!WeaponObject.IsSwinging)
        {
            WpnObject.transform.position = new Vector2(transform.position.x + Mathf.Cos(MouseAngle) * CosmeticWeaponDistance, transform.position.y + Mathf.Sin(MouseAngle) * CosmeticWeaponDistance);
            WpnObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }

        //Dont create something new; use WeaponObject instead
        //Somehow implement it so that its not a Prefab
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
