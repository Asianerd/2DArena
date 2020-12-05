using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponObject : MonoBehaviour
{
    List<IEnumerator> weaponObjectBehaviours;
    WeaponData.Weapon weapon;
    public static bool isSwinging;

    void Update()
    {
        weapon = PlayerGeneral.currentWeaponReference;
        SpriteCheck();
    }

    void SpriteCheck()
    {
        switch(weapon.category)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalRangeWeaponSpriteList[weapon.weaponID];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalProjectileSpriteList[weapon.weaponID];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = WeaponData.globalMeleeWeaponSpriteList[weapon.weaponID];
                break;
        }
    }

    public void SwingWrapper()
    {
        if(!isSwinging)
            StartCoroutine(Swing());
    }

    // Swing
    IEnumerator Swing()
    {
        isSwinging = true;

        int CurrentCooldown = weapon.weaponCooldown;

        //float IncrementAngle = 125f / Weapon.WeaponCooldown;
        float IncrementAngle = PlayerGeneral.weaponObjectIsFlipped ? 125f / weapon.weaponCooldown : -(125f / weapon.weaponCooldown);

        while (CurrentCooldown > 0)
        {
            CurrentCooldown--;

            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle));
            //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0f, 0f, EndRotating, 0f), 0.125f);
            //transform.rotation = Quaternion.Lerp(transform.rotation,new Quaternion(0f,0f,EndRotating))
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle);
            yield return null;
        }
        isSwinging = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !InventoryGeneral.gamePaused && isSwinging)
        {
            float MinDamage, MaxDamage;
            MinDamage = weapon.damageMin * weapon.level;
            MaxDamage = weapon.damageMax * weapon.level;
            double DamageInflicted = UnityEngine.Random.Range(MinDamage, MaxDamage);
            collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, weapon.knockback, transform.position);
        }
    }
}
