using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    WeaponData.Weapon Weapon;
    int CurrentCooldown;


    // 0 - Swing
    public float TotalAngle = 130;
    public float IncrementAngle;
    public float OffsetX, OffsetY;

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
        if (!InventoryShow.GamePaused && IsSwinging)
        {
            switch (Weapon.ScriptID)
            {
                case 1:
                    //Stab
                    transform.position = Vector2.MoveTowards(transform.position, Target, Speed);

                    CurrentCooldown++;
                    if (CurrentCooldown >= PlayerGeneral.CurrentWeaponReference.WeaponCooldown)
                    {
                        IsSwinging = false;
                        EnemyAttackedList = new List<GameObject>();
                    }
                    break;
                case 2:
                    if (NearestEnemy != null)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, NearestEnemy.transform.position, Weapon.Knockback);

                        CurrentCooldown++;
                        if (CurrentCooldown >= PlayerGeneral.CurrentWeaponReference.WeaponCooldown)
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
                    transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - IncrementAngle);
                    transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + OffsetX, PlayerGeneral.PlayerPosition.y + OffsetY);

                    CurrentCooldown++;
                    if (CurrentCooldown >= PlayerGeneral.CurrentWeaponReference.WeaponCooldown)
                    {
                        IsSwinging = false;
                        EnemyAttackedList = new List<GameObject>();
                    }
                    break;
            }
        }
    }



    void OnTriggerStay2D(Collider2D Collision)
    {
        if (Collision.CompareTag("Enemy") && !InventoryShow.GamePaused && IsSwinging)
        {
            switch (Weapon.ScriptID)
            {
                case 2:
                    if (NearestEnemy.GetComponent<EnemyGeneral>().HP > 0)
                    {
                        double _DamageInflicted = UnityEngine.Random.Range(PlayerGeneral.CurrentWeaponReference.DamageMin, PlayerGeneral.CurrentWeaponReference.DamageMax);
                        Collision.GetComponent<EnemyGeneral>().MinusHealth(_DamageInflicted, PlayerGeneral.CurrentWeaponReference.Knockback, PlayerGeneral.PlayerPosition);
                        EnemyAttackedList.Add(Collision.gameObject);
                    }
                    else
                    {
                        CurrentCooldown = Weapon.WeaponCooldown;
                    }
                    break;
                default:
                    if (!EnemyAttackedList.Contains(Collision.gameObject))
                    {
                        double DamageInflicted = UnityEngine.Random.Range(PlayerGeneral.CurrentWeaponReference.DamageMin, PlayerGeneral.CurrentWeaponReference.DamageMax);
                        Collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, PlayerGeneral.CurrentWeaponReference.Knockback, PlayerGeneral.PlayerPosition);
                        EnemyAttackedList.Add(Collision.gameObject);
                    }
                    break;
            }
        }
    }

    public void Swing()
    {
        Weapon = PlayerGeneral.CurrentWeaponReference;
        switch(Weapon.ScriptID)
        {
            case 1:
                Target = new Vector2(Mathf.Cos(PlayerGeneral.MouseAngle)*100,Mathf.Sin(PlayerGeneral.MouseAngle)*100);
                Speed = PlayerGeneral.CurrentWeaponReference.Knockback;
                break;
            case 2:
                EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
                if (EnemyList.Length > 0)
                {
                    NearestEnemy = EnemyList[0];
                    foreach (GameObject i in EnemyList)
                    {
                        if (Vector2.Distance(PlayerGeneral.PlayerPosition, i.transform.position) <= Vector2.Distance(PlayerGeneral.PlayerPosition, NearestEnemy.transform.position)) 
                        {
                            NearestEnemy = i;
                        }
                    }
                }
                break;
            default:
                OffsetX = Mathf.Cos(PlayerGeneral.MouseAngle) * 0.5f;
                OffsetY = Mathf.Sin(PlayerGeneral.MouseAngle) * 0.5f;
                IncrementAngle = TotalAngle / PlayerGeneral.CurrentWeaponReference.WeaponCooldown; // Change this ; Speed(TotalAngle) is determined by the knockback
                break;
        }

        //Look towards (2D version) - start
        transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + OffsetX , PlayerGeneral.PlayerPosition.y + OffsetX);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //Look towards - end

        IsSwinging = true;
        CurrentCooldown = 0;
    }
}
