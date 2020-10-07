using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public GameObject Player;
    public WeaponData.Weapon Weapon;
    float TotalAngle = 130;
    int CurrentCooldown = 0;
    float IncrementAngle;


    void Update()
    {
        if (!InventoryShow.GamePaused)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - IncrementAngle);
            CurrentCooldown++;
            if (CurrentCooldown >= Weapon.WeaponCooldown) Destroy(gameObject);

            transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + 0.5f, PlayerGeneral.PlayerPosition.y + 0.5f);
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if(Collision.CompareTag("Enemy") && !InventoryShow.GamePaused)
        {
            double DamageInflicted = UnityEngine.Random.Range(Weapon.DamageMin, Weapon.DamageMax);
            Collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted,Weapon.Knockback,PlayerGeneral.PlayerPosition);
        }
    }

    public void Set(WeaponData.Weapon weapon)
    {
        transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + 0.5f, PlayerGeneral.PlayerPosition.y + 0.5f);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z-90);

        Weapon = weapon;
        IncrementAngle = TotalAngle / Weapon.WeaponCooldown; // Change this ; Speed(TotalAngle) is determined by the knockback
    }
}
