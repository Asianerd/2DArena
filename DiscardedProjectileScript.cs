using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DiscardedProjectileScript : MonoBehaviour
{
    //This was just copied from RangeProjectileScript.cs so some bugs and errors might exist
    WeaponData.Weapon ProjectileWeapon;
    double DirectionAngle;
    float Speed;
    int ShelfLife;
    double TargetX, TargetY;
    bool HasDied = false;
    public GameObject Runtime;
    public GameObject Player;


    public void Set(WeaponData.Weapon weapon,GameObject playerobject)
    {
        DirectionAngle = PlayerGeneral.MouseAngle;
        Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife;
        TargetX = Math.Cos(DirectionAngle) * 10000;
        TargetY = Math.Sin(DirectionAngle) * 10000;
        ProjectileWeapon = new WeaponData.Weapon(weapon);
        Player = playerobject;
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");

        GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.ProjectileSpriteID];
        GetComponent<CapsuleCollider2D>().size = GetComponent<SpriteRenderer>().sprite.rect.size/10;


        //Look at
        Vector3 diff = new Vector3(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        //
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = UnityEngine.Random.Range(ProjectileWeapon.DamageMin, ProjectileWeapon.DamageMax);
        GameObject CollidedObject = collision.gameObject;
        if (CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, ProjectileWeapon.Knockback, transform.position);
            Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
            Die(Convert.ToInt32(DamageInflicted));
        }
        if (CollidedObject.CompareTag("Solid"))
        {
            Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
            Die(Convert.ToInt32(DamageInflicted));
        }
    }

    public void Die(int DamageInflicted)
    {
        if (!HasDied)
        {
            Player.GetComponent<PlayerGeneral>().AddWeaponLevelProgress(DamageInflicted);
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
                if(ProjectileWeapon.ProjectileSpin)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.eulerAngles.z + ProjectileWeapon.ProjectileSpinSpeed);
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
