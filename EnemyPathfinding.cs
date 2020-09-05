using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float EnemySpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, EnemySpeed);
    }
}