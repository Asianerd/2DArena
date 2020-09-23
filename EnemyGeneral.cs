using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public double HP = 100;
    public float EnemyDist;
    public GameObject runtime;

    private void Start()
    {
        runtime = GameObject.FindGameObjectWithTag("RuntimeScript");
    }

    void Update() 
    {
        if (HP <= 0)
        {
            //GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position.x, transform.position.y, UnityEngine.Random.Range(0, 2));
            runtime.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position.x, transform.position.y, UnityEngine.Random.Range(0,57));
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
