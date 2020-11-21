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
    public float DamageMin, DamageMax;
    public float Speed;
    public int ShelfLife;
    float TargetX,TargetY;
    bool HasDied = false;


    public void Set(WeaponData.Weapon Weapon, GameObject Plyr)
    {
        DirectionAngle = PlayerGeneral.MouseAngle;
        ProjectileWeapon = Weapon;
        TargetX = Convert.ToSingle(Math.Cos(DirectionAngle) * 10000);
        TargetY = Convert.ToSingle(Math.Sin(DirectionAngle) * 10000);
        Player = Plyr;
        //GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.GlobalRangeWeaponProjectileSpriteList[weapon.ProjectileSpriteID; Its going to be set in the SpriteRenderer (cos theres gonna be multiple Projectile prefabs)
        //GetComponent<CapsuleCollider2D>().size = GetComponentInChildren<SpriteRenderer>().sprite.rect.size/10;


        /*Vector3 diff = new Vector3(TargetX,TargetY) - transform.position;
        //diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/
        transform.rotation = Quaternion.Euler(0f, 0f, PlayerGeneral.MouseAngle * Mathf.Rad2Deg);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = (UnityEngine.Random.Range(DamageMin, DamageMax))*ProjectileWeapon.Level;
        GameObject CollidedObject = collision.gameObject;
        if(CollidedObject.CompareTag("Enemy"))
        {
            if (!HasDied)
            {
                CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, ProjectileWeapon.Knockback, transform.position);
                Player.GetComponent<PlayerGeneral>().MinusWeaponDurability();
                Player.GetComponent<PlayerGeneral>().AddWeaponLevelProgress(Convert.ToInt32(DamageInflicted));
            }
            HasDied = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!InventoryGeneral.GamePaused)
        {
            if (ShelfLife > 0)
            {
                ShelfLife--;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(TargetX, TargetY), Speed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
