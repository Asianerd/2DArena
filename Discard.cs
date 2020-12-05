using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Discard : MonoBehaviour
{
    WeaponData.Weapon Weapon;
    int CurrentCooldown;
    GameObject Runtime;
    GameObject Player;

    // 0 - Swing
    public float TotalIncrement = 130;
    public float IncrementAngle;
    public float OffsetX, OffsetY;
    public float IncrementAmount;

    // 1 - Stab
    public Vector2 Target;
    public float Speed;

    // 2 - Auto-aim
    public GameObject[] EnemyList = { };
    public GameObject NearestEnemy;


    public static bool IsSwinging;

    public List<GameObject> EnemyAttackedList = new List<GameObject>();

    void Update()
    {
        switch (PlayerGeneral.currentWeaponReference.category)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalRangeWeaponSpriteList[PlayerGeneral.currentWeaponReference.weaponID];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalProjectileSpriteList[PlayerGeneral.currentWeaponReference.projectileSpriteID];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalMeleeWeaponSpriteList[PlayerGeneral.currentWeaponReference.weaponID];
                break;
        }
        if (!InventoryGeneral.gamePaused && IsSwinging)
        {
            switch (Weapon.scriptID)
            {
                case 1:
                    //Stab
                    transform.position = Vector2.MoveTowards(transform.position, Target, Speed);

                    CurrentCooldown++;
                    if (CurrentCooldown >= PlayerGeneral.currentWeaponReference.weaponCooldown)
                    {
                        IsSwinging = false;
                        EnemyAttackedList = new List<GameObject>();
                    }
                    break;
                case 2:
                    //Auto-aim
                    if (NearestEnemy != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, NearestEnemy.transform.position, Weapon.knockback);

                        CurrentCooldown++;
                        if (CurrentCooldown >= PlayerGeneral.currentWeaponReference.weaponCooldown)
                        {
                            IsSwinging = false;
                            EnemyAttackedList = new List<GameObject>();
                        }
                    }
                    else
                    {
                        IsSwinging = false;
                        EnemyAttackedList = new List<GameObject>();
                    }
                    break;
                default:
                    //Swing
                    if (IncrementAmount <= TotalIncrement)
                    {
                        IncrementAmount += Weapon.knockback;
                        if (PlayerGeneral.weaponObjectIsFlipped)
                            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle);
                        else
                            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - IncrementAngle);
                        transform.position = new Vector2(PlayerGeneral.playerPosition.x + OffsetX, PlayerGeneral.playerPosition.y + OffsetY);
                    }
                    CurrentCooldown++;
                    if (CurrentCooldown >= PlayerGeneral.currentWeaponReference.weaponCooldown)
                    {
                        IncrementAmount = 0;
                        IsSwinging = false;
                        EnemyAttackedList = new List<GameObject>();
                    }
                    break;
            }
        }
    }



    void OnTriggerStay2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Enemy") && !InventoryGeneral.gamePaused && IsSwinging)
        {
            switch (Weapon.scriptID)
            {
                case 2:
                    if (NearestEnemy.GetComponent<EnemyGeneral>().health > 0)
                    {
                        double _DamageInflicted = UnityEngine.Random.Range(PlayerGeneral.currentWeaponReference.damageMin, PlayerGeneral.currentWeaponReference.damageMax);
                        Collision.GetComponent<EnemyGeneral>().MinusHealth(_DamageInflicted, PlayerGeneral.currentWeaponReference.knockback, PlayerGeneral.playerPosition);
                        EnemyAttackedList.Add(Collision.gameObject);
                        if (PlayerGeneral.currentWeaponReference.isBreakable) { Player.GetComponent<PlayerGeneral>().MinusWeaponDurability(); }
                    }
                    else
                    {
                        CurrentCooldown = Weapon.weaponCooldown;
                    }
                    break;
                default:
                    if (!EnemyAttackedList.Contains(Collision.gameObject))
                    {
                        double DamageInflicted = UnityEngine.Random.Range(PlayerGeneral.currentWeaponReference.damageMin, PlayerGeneral.currentWeaponReference.damageMax);
                        Collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, PlayerGeneral.currentWeaponReference.knockback, PlayerGeneral.playerPosition);
                        EnemyAttackedList.Add(Collision.gameObject);
                        if (PlayerGeneral.currentWeaponReference.isBreakable) { Player.GetComponent<PlayerGeneral>().MinusWeaponDurability(); }
                    }
                    break;
            }
        }
    }

    void Awake()
    {
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Swing()
    {
        Weapon = PlayerGeneral.currentWeaponReference;
        switch (Weapon.scriptID)
        {
            case 1:
                //Stab
                Target = new Vector2(Mathf.Cos(PlayerGeneral.mouseAngle) * 100, Mathf.Sin(PlayerGeneral.mouseAngle) * 100);
                Speed = PlayerGeneral.currentWeaponReference.knockback;
                break;
            case 2:
                //Auto-aim
                EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
                if (EnemyList.Length > 0)
                {
                    NearestEnemy = EnemyList[0];
                    foreach (GameObject i in EnemyList)
                    {
                        if (Vector2.Distance(PlayerGeneral.playerPosition, i.transform.position) <= Vector2.Distance(PlayerGeneral.playerPosition, NearestEnemy.transform.position))
                        {
                            NearestEnemy = i;
                        }
                    }
                }
                break;
            default:
                OffsetX = Mathf.Cos(PlayerGeneral.mouseAngle) * 0.5f;
                OffsetY = Mathf.Sin(PlayerGeneral.mouseAngle) * 0.5f;
                IncrementAngle = Weapon.knockback * 7; // Change this ; Speed(TotalAngle) is determined by the knockback
                IncrementAmount = 0;
                break;
        }

        //Look towards (2D version) - start
        transform.position = new Vector2(PlayerGeneral.playerPosition.x + OffsetX, PlayerGeneral.playerPosition.y + OffsetX);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //Look towards - end

        IsSwinging = true;
        CurrentCooldown = 0;
    }
}
