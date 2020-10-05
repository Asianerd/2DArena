using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public double HP = 100;
    public float EnemyDist;
    public GameObject runtime;
    public GameObject Player;
    public GameObject DamageBubblePrefab;


    void Awake()
    {
        runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        if (HP <= 0)
        {
            int SpawnedWeaponID = UnityEngine.Random.Range(0, WeaponData.GlobalWeaponList.Count);
            runtime.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position.x, transform.position.y, WeaponData.GlobalWeaponList[SpawnedWeaponID],SpawnedWeaponID);
            Destroy(gameObject);
        }
    }
    public void MinusHealth(double LostHealth,float Knockback,Vector2 DamageSource)
    {
        double angle = Math.Atan2(transform.position.y - DamageSource.y, transform.position.x - DamageSource.x);
        HP -= LostHealth;
        float targetx = Convert.ToSingle((Math.Cos(angle) * Knockback) + transform.position.x);
        float targety = Convert.ToSingle((Math.Sin(angle) * Knockback) + transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetx, targety), Knockback);
        Instantiate(DamageBubblePrefab, transform.position, Quaternion.identity).GetComponent<FXDamageBubbleGeneral>().Damage = Convert.ToSingle(LostHealth);
    }
    public float DistanceEnemy(float SourceX, float SourceY)
    {
        return Vector2.Distance(new Vector3(SourceX, SourceY, 0f), new Vector3(transform.position.x, transform.position.y, 0f));
    }
}
