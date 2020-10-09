using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GenericRangeProjectile : MonoBehaviour
{
    WeaponData.Weapon ProjectileWeapon;
    double DirectionAngle;
    float Speed;
    int ShelfLife;
    double TargetX,TargetY;
    public GameObject Runtime;


    public void Set(double angle,WeaponData.Weapon weapon)
    {
        DirectionAngle = angle;
        Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife;
        TargetX = Math.Cos(DirectionAngle) * 10000;
        TargetY = Math.Sin(DirectionAngle) * 10000;
        ProjectileWeapon = weapon;
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        GetComponentInChildren<SpriteRenderer>().sprite = Runtime.GetComponent<WeaponData>().RangeWeaponProjectileList[weapon.ProjectileSpriteID];
        
        //GameObject.FindGameObjectWithTag("RuntimeScript").GetComponent<WeaponData>().RangeWeaponProjectileList[SpriteID];
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        double DamageInflicted = UnityEngine.Random.Range(ProjectileWeapon.DamageMin, ProjectileWeapon.DamageMax);
        GameObject CollidedObject = collision.gameObject;
        if(CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted,ProjectileWeapon.Knockback,transform.position);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!InventoryShow.GamePaused)
        {
            if (ShelfLife > 0)
            {
                ShelfLife--;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)), Speed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
