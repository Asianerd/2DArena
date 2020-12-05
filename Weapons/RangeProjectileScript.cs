using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RangeProjectileScript : MonoBehaviour
{
    WeaponData.Weapon projectileWeapon;
    GameObject player;

    // Straight
    double directionAngle;
    public float damageMin, damageMax;
    public float speed;
    public int shelfLife;
    float targetX, targetY;
    bool hasDied = false;


    public void Set(WeaponData.Weapon Weapon, GameObject Plyr)
    {
        directionAngle = PlayerGeneral.mouseAngle;
        projectileWeapon = Weapon;
        targetX = Convert.ToSingle(Math.Cos(directionAngle) * 10000);
        targetY = Convert.ToSingle(Math.Sin(directionAngle) * 10000);
        player = Plyr;
        //GetComponentInChildren<SpriteRenderer>().sprite = WeaponData.GlobalRangeWeaponProjectileSpriteList[weapon.ProjectileSpriteID; Its going to be set in the SpriteRenderer (cos theres gonna be multiple Projectile prefabs)
        //GetComponent<CapsuleCollider2D>().size = GetComponentInChildren<SpriteRenderer>().sprite.rect.size/10;


        /*Vector3 diff = new Vector3(TargetX,TargetY) - transform.position;
        //diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);*/
        transform.rotation = Quaternion.Euler(0f, 0f, PlayerGeneral.mouseAngle * Mathf.Rad2Deg);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        double DamageInflicted = (UnityEngine.Random.Range(damageMin, damageMax))*projectileWeapon.level;
        GameObject CollidedObject = collision.gameObject;
        if(CollidedObject.CompareTag("Enemy"))
        {
            if (!hasDied)
            {
                CollidedObject.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, projectileWeapon.knockback, transform.position);
                player.GetComponent<PlayerGeneral>().MinusWeaponDurability();
                player.GetComponent<PlayerGeneral>().AddWeaponLevelProgress(Convert.ToInt32(DamageInflicted));
            }
            hasDied = true;
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!InventoryGeneral.gamePaused)
        {
            if (shelfLife > 0)
            {
                shelfLife--;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, targetY), speed);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
