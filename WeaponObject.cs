using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponObject : MonoBehaviour
{
    List<IEnumerator> WeaponObjectBehaviours;
    WeaponData.Weapon Weapon;
    public static bool IsSwinging;

    void Update()
    {
        Weapon = PlayerGeneral.CurrentWeaponReference;
    }

    void SpriteCheck()
    {
        switch(Weapon.Category)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = WeaponData.GlobalRangeWeaponProjectileSpriteList[Weapon.WeaponID];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = WeaponData.GlobalProjectileSpriteList[Weapon.WeaponID];
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = WeaponData.GlobalMeleeWeaponSpriteList[Weapon.WeaponID];
                break;
        }
    }

    public void SwingWrapper()
    {
        if(!IsSwinging)
            StartCoroutine(Swing());
    }

    // Swing
    IEnumerator Swing()
    {
        IsSwinging = true;

        int CurrentCooldown = Weapon.WeaponCooldown;

        Quaternion TargetRotation = Quaternion.Euler(0f, 0f, 0f);

        while (CurrentCooldown > 0)
        {
            CurrentCooldown--;

            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle));
            //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0f, 0f, EndRotating, 0f), 0.125f);
            //transform.rotation = Quaternion.Lerp(transform.rotation,new Quaternion(0f,0f,EndRotating))
            transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Weapon.Knockback);
            yield return null;
        }
        IsSwinging = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !InventoryGeneral.GamePaused && IsSwinging)
        {
            float MinDamage, MaxDamage;
            MinDamage = Weapon.DamageMin * Weapon.Level;
            MaxDamage = Weapon.DamageMax * Weapon.Level;
            double DamageInflicted = UnityEngine.Random.Range(MinDamage, MaxDamage);
            collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, Weapon.Knockback, transform.position);
        }
    }
}
