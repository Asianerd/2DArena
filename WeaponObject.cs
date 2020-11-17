using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class WeaponObject : MonoBehaviour
{
    public SpriteRenderer WpnObjectSpriteRenderer;

    List<IEnumerator> WeaponObjectBehaviours;
    GameObject Weapon;
    public static bool IsSwinging;
    public static bool IsFlipped = false;
    float WeaponDistance = 1f;
    public float MaxSwing = 125f;

    void Update()
    {
        //Debug.Log(PlayerGeneral.MouseAngle * Mathf.Rad2Deg);
        Weapon = PlayerGeneral.CurrentWeaponReference;
        PositionWeapon();
        if (IsSwinging)
        {

        }
        else
        {
            LookAtMouse();
        }
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
        Quaternion InitAngle = new Quaternion(0f, 0f, (PlayerGeneral.MouseAngle) * Mathf.Rad2Deg, 0f);
        Quaternion EndAngle = new Quaternion(0f, 0f, InitAngle.z + (MaxSwing / 2), 0f);
        float TotalRotated = 0;

        int CurrentCooldown = Weapon.WeaponCooldown;

        while (CurrentCooldown > 0)
        {
            CurrentCooldown--;
            TotalRotated += Weapon.Knockback;
            if (TotalRotated >= 125)
            {
                break;
            }

            transform.rotation = Quaternion.Euler(0f, 0f, (transform.rotation.eulerAngles.z + Weapon.Knockback));
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

    void PositionWeapon()
    {
        if (!IsSwinging)
        {
            // Moves according to the mouse angle
            transform.position = (new Vector2(Mathf.Cos(PlayerGeneral.MouseAngle) * WeaponDistance, Mathf.Sin(PlayerGeneral.MouseAngle) * WeaponDistance))+PlayerGeneral.PlayerPosition;
        }
    }

    void LookAtMouse()
    {
        // Looks at mouse and flips weapon object according to the weapon object angle
        // Look at mouse
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        if ((Math.Abs(PlayerGeneral.MouseAngle * Mathf.Rad2Deg)) >= 90)
        {
            // Mouse is on left side of player
            transform.localScale = new Vector3(1f, -1f, 1f);
            IsFlipped = true;
        }
        else
        {
            // Mouse is on right side of player
            transform.localScale = new Vector3(1f, 1f, 1f);
            IsFlipped = false;
        }
    }
}
