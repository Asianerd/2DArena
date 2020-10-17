using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RangeProjectileScript : MonoBehaviour
{
    WeaponData.Weapon ProjectileWeapon;
    GameObject Player;

    // Straight
    double DirectionAngle;
    float Speed;
    int ShelfLife;
    double TargetX,TargetY;
    public GameObject Runtime;


    public void Set(WeaponData.Weapon weapon,GameObject Plyr)
    {
        DirectionAngle = PlayerGeneral.MouseAngle;
        Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife;
        TargetX = Math.Cos(DirectionAngle) * 10000;
        TargetY = Math.Sin(DirectionAngle) * 10000;
        ProjectileWeapon = weapon;
        Player = Plyr;
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        GetComponentInChildren<SpriteRenderer>().sprite = Runtime.GetComponent<WeaponData>().RangeWeaponProjectileList[weapon.ProjectileSpriteID];
        GetComponent<CapsuleCollider2D>().size = GetComponentInChildren<SpriteRenderer>().sprite.rect.size/10;


        Vector3 diff = new Vector3(Convert.ToSingle(TargetX),Convert.ToSingle(TargetY)) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = UnityEngine.Random.Range(ProjectileWeapon.DamageMin, ProjectileWeapon.DamageMax);
        GameObject CollidedObject = collision.gameObject;
        if(CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted,ProjectileWeapon.Knockback,transform.position);
            Player.GetComponent<PlayerGeneral>().MinusWeaponDurability();
            Destroy(gameObject);
        }
        if(CollidedObject.CompareTag("Solid"))
        {
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
