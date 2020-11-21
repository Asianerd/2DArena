using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //This was just copied from RangeProjectileScript.cs so some bugs and errors might exist
    // This is attached to the Projectile GameObject Prefab (the one spawned when shooting)
    WeaponData.Weapon ProjectileWeapon;
    double DirectionAngle;
    public float DamageMin, DamageMax;
    public float Speed;
    public int ShelfLife;
    public bool ProjectileSpin;
    public float ProjectileSpinSpeed;
    double TargetX, TargetY;
    bool HasDied = false;
    public GameObject Player;


    public void Set(WeaponData.Weapon weapon,GameObject playerobject)
    {
        DirectionAngle = PlayerGeneral.MouseAngle;
        /* Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife; */
        TargetX = Math.Cos(DirectionAngle) * 10000;
        TargetY = Math.Sin(DirectionAngle) * 10000;
        ProjectileWeapon = weapon;
        Player = playerobject;

        //GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.ProjectileSpriteID];
        //GetComponent<CapsuleCollider2D>().size = GetComponent<SpriteRenderer>().sprite.rect.size/10;



        /*Vector3 diff = new Vector3(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/
        transform.rotation = Quaternion.Euler(0f, 0f, PlayerGeneral.MouseAngle * Mathf.Rad2Deg);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = (UnityEngine.Random.Range(DamageMin, DamageMax)*ProjectileWeapon.Level);
        GameObject CollidedObject = collision.gameObject;
        if (CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, ProjectileWeapon.Knockback, transform.position);
            Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
            Die(Convert.ToInt32(DamageInflicted));
        }
    }

    public void Die(int DamageInflicted)
    {
        if (!HasDied)
        {
            Player.GetComponent<PlayerGeneral>().AddWeaponLevelProgress(DamageInflicted);
            PlayerGeneral.MinusProjectile();
            Destroy(gameObject);
            HasDied = true;
        }
    }


    void Update()
    {
        if (!InventoryGeneral.GamePaused)
        {
            if (ShelfLife > 0)
            {
                ShelfLife--;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)), Speed);
                if(ProjectileSpin)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.eulerAngles.z + ProjectileSpinSpeed); // Spins the projectile
                }
            }
            else
            {
                Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
                Die(0);
            }
        }
    }
}
