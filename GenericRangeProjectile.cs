using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GenericRangeProjectile : MonoBehaviour
{
    public WeaponData.Weapon ProjectileWeapon;
    public double DirectionAngle;
    public float Speed;
    public int ShelfLife;
    public double TargetX,TargetY;

    public void Set(double angle,float speed,int shelflife,WeaponData.Weapon weapon)
    {
        DirectionAngle = angle;
        Speed = speed;
        ShelfLife = shelflife;
        TargetX = Math.Cos(DirectionAngle) * 100;
        TargetY = Math.Sin(DirectionAngle) * 100;
        ProjectileWeapon = weapon;
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
        if (ShelfLife > 0)
        {
            ShelfLife--;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(Convert.ToSingle(TargetX),Convert.ToSingle(TargetY)),Speed/10);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
