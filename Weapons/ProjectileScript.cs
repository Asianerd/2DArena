using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //This was just copied from RangeProjectileScript.cs so some bugs and errors might exist
    // This is attached to the Projectile GameObject Prefab (the one spawned when shooting)
    WeaponData.Weapon projectileWeapon;
    double directionAngle;
    public float damageMin, damageMax;
    public float speed;
    public int shelfLife;
    public bool projectileSpin;
    public float projectileSpinSpeed;
    double targetX, targetY;
    bool hasDied = false;
    public GameObject player;


    public void Set(WeaponData.Weapon weapon,GameObject playerobject)
    {
        directionAngle = PlayerGeneral.mouseAngle;
        /* Speed = weapon.ProjectileSpeed;
        ShelfLife = weapon.ShelfLife; */
        targetX = Math.Cos(directionAngle) * 10000;
        targetY = Math.Sin(directionAngle) * 10000;
        projectileWeapon = weapon;
        player = playerobject;

        //GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.GlobalProjectileSpriteList[weapon.ProjectileSpriteID];
        //GetComponent<CapsuleCollider2D>().size = GetComponent<SpriteRenderer>().sprite.rect.size/10;



        /*Vector3 diff = new Vector3(Convert.ToSingle(TargetX), Convert.ToSingle(TargetY)) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/
        transform.rotation = Quaternion.Euler(0f, 0f, PlayerGeneral.mouseAngle * Mathf.Rad2Deg);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = (UnityEngine.Random.Range(damageMin, damageMax)*projectileWeapon.level);
        GameObject CollidedObject = collision.gameObject;
        if (CollidedObject.CompareTag("Enemy"))
        {
            CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, projectileWeapon.knockback, transform.position);
            player.GetComponent<PlayerGeneral>().projectileSpawned.Remove(gameObject);
            Die(Convert.ToInt32(DamageInflicted));
        }
    }

    public void Die(int DamageInflicted)
    {
        if (!hasDied)
        {
            player.GetComponent<PlayerGeneral>().AddWeaponLevelProgress(DamageInflicted);
            PlayerGeneral.MinusProjectile();
            Destroy(gameObject);
            hasDied = true;
        }
    }


    void Update()
    {
        if (!InventoryGeneral.gamePaused)
        {
            if (shelfLife > 0)
            {
                shelfLife--;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Convert.ToSingle(targetX), Convert.ToSingle(targetY)), speed);
                if(projectileSpin)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.eulerAngles.z + projectileSpinSpeed); // Spins the projectile
                }
            }
            else
            {
                player.GetComponent<PlayerGeneral>().projectileSpawned.Remove(gameObject);
                Die(0);
            }
        }
    }
}
