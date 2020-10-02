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

    public void Awake()
    {
        runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() 
    {
        if (HP <= 0)
        {
            int SpawnedWeaponID = UnityEngine.Random.Range(0, Player.GetComponent<PlayerGeneral>().GlobalWeaponList.Count);
            runtime.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position.x, transform.position.y, Player.GetComponent<PlayerGeneral>().GlobalWeaponList[SpawnedWeaponID],SpawnedWeaponID);
            Destroy(gameObject);
        }
    }
    public void MinusHealth(double LostHealth)
    {
        HP -= LostHealth;
    }
    public float DistanceEnemy(float SourceX, float SourceY)
    {
        return Vector2.Distance(new Vector3(SourceX, SourceY, 0f), new Vector3(transform.position.x, transform.position.y, 0f));
    }
}
