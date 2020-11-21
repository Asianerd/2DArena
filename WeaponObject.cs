using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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
        SpriteCheck();
    }

    void SpriteCheck()
    {
        switch(Weapon.Category)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = WeaponData.GlobalRangeWeaponSpriteList[Weapon.WeaponID];
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

        //float IncrementAngle = 125f / Weapon.WeaponCooldown;
        float IncrementAngle = PlayerGeneral.WeaponObjectIsFlipped ? 125f / Weapon.WeaponCooldown : -(125f / Weapon.WeaponCooldown);
        Debug.Log($"{IncrementAngle} :: {Weapon.WeaponCooldown}");

        while (CurrentCooldown > 0)
        {
            CurrentCooldown--;

            //transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle));
            //transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0f, 0f, EndRotating, 0f), 0.125f);
            //transform.rotation = Quaternion.Lerp(transform.rotation,new Quaternion(0f,0f,EndRotating))
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + IncrementAngle);
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
