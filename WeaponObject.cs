using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public GameObject Player;
    float TotalAngle = 130;
    int CurrentCooldown = 0;
    float IncrementAngle;
    float OffsetX, OffsetY;
    public static bool IsSwinging;

    public List<GameObject> EnemyAttackedList = new List<GameObject>();


    void Update()
    {
        if (!InventoryShow.GamePaused && IsSwinging)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z - IncrementAngle);
            CurrentCooldown++;
            if (CurrentCooldown >= Player.GetComponent<PlayerGeneral>().CurrentWeapon.WeaponCooldown)
            {
                IsSwinging = false;
                EnemyAttackedList = new List<GameObject>();
            }

            transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + OffsetX, PlayerGeneral.PlayerPosition.y + OffsetY);
        }
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }



    void OnTriggerStay2D(Collider2D Collision)
    {
        if(Collision.CompareTag("Enemy") && !InventoryShow.GamePaused && IsSwinging)
        {
            if (!EnemyAttackedList.Contains(Collision.gameObject))
            {
                double DamageInflicted = UnityEngine.Random.Range(PlayerGeneral.CurrentWeaponReference.DamageMin, PlayerGeneral.CurrentWeaponReference.DamageMax);
                Collision.GetComponent<EnemyGeneral>().MinusHealth(DamageInflicted, PlayerGeneral.CurrentWeaponReference.Knockback, PlayerGeneral.PlayerPosition);
                EnemyAttackedList.Add(Collision.gameObject);
            }
        }
    }

    public void Swing()
    {
        OffsetX = Mathf.Cos(PlayerGeneral.MouseAngle)*0.5f;
        OffsetY = Mathf.Sin(PlayerGeneral.MouseAngle)*0.5f;

        //Look towards (2D version)
        transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + OffsetX , PlayerGeneral.PlayerPosition.y + OffsetX);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        //

        IncrementAngle = TotalAngle / PlayerGeneral.CurrentWeaponReference.WeaponCooldown; // Change this ; Speed(TotalAngle) is determined by the knockback
        IsSwinging = true;
        CurrentCooldown = 0;
    }
}
