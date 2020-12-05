using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public double health = 100;
    public float enemyDist;
    public GameObject runtime;
    public GameObject player;
    public GameObject damageBubblePrefab;


    void Awake()
    {
        runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        if (health <= 0)
        {
            int SpawnedWeaponID = UnityEngine.Random.Range(0, WeaponData.globalWeaponList.Count);
            runtime.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position, WeaponData.globalWeaponList[SpawnedWeaponID]);
            Destroy(gameObject);
        }
    }
    public void MinusHealth(double LostHealth,float Knockback,Vector2 DamageSource)
    {
        double angle = Math.Atan2(transform.position.y - DamageSource.y, transform.position.x - DamageSource.x);
        health -= LostHealth;
        float targetx = Convert.ToSingle((Math.Cos(angle) * Knockback) + transform.position.x);
        float targety = Convert.ToSingle((Math.Sin(angle) * Knockback) + transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetx, targety), Knockback);
        Instantiate(damageBubblePrefab, transform.position, Quaternion.identity).GetComponent<FXDamageBubbleGeneral>().damage = Convert.ToSingle(LostHealth);
    }
    public float DistanceEnemy(float SourceX, float SourceY)
    {
        return Vector2.Distance(new Vector3(SourceX, SourceY, 0f), new Vector3(transform.position.x, transform.position.y, 0f));
    }
}
