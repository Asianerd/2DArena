using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    public GameObject player; //Fetched in Start()
    public float EnemySpeed = 0.005f;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, EnemySpeed);
    }
}
