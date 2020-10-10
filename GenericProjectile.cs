using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GenericProjectile : MonoBehaviour
{
    //This was just copied from GenericRangeProjectile.cs so some bugs and errors might exist
    WeaponData.Weapon ProjectileWeapon;
    double DirectionAngle;
    float Speed;
    int ShelfLife;
    double TargetX, TargetY;
    public GameObject Runtime;
    public GameObject Player;


    public void Set(WeaponData.Weapon weapon,GameObject ParentObj)
    {
        DirectionAngle = PlayerGeneral.MouseAngle;
        Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife;
        TargetX = Math.Cos(DirectionAngle) * 10000;
        TargetY = Math.Sin(DirectionAngle) * 10000;
        ProjectileWeapon = weapon;
        Runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        Player = ParentObj;
        GetComponentInChildren<SpriteRenderer>().sprite = Runtime.GetComponent<WeaponData>().ProjectileSpriteList[weapon.ProjectileSpriteID];

        Vector3 diff = new Vector3(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = UnityEngine.Random.Range(ProjectileWeapon.DamageMin, ProjectileWeapon.DamageMax);
        GameObject CollidedObject = collision.gameObject;
        if (CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, ProjectileWeapon.Knockback, transform.position);
            Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
            Player.GetComponent<PlayerGeneral>().ProjectileAmountUsed--;
            Destroy(gameObject);
        }
        if (CollidedObject.CompareTag("Solid"))
        {
            Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
            Player.GetComponent<PlayerGeneral>().ProjectileAmountUsed--;
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
                if(ProjectileWeapon.ProjectileSpin)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.eulerAngles.z + ProjectileWeapon.ProjectileSpinSpeed);
                }
            }
            else
            {
                Player.GetComponent<PlayerGeneral>().ProjectileSpawned.Remove(gameObject);
                Player.GetComponent<PlayerGeneral>().ProjectileAmountUsed--;
                Destroy(gameObject);
            }
        }
    }
}
