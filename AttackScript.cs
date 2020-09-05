using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float AttackRange = 3;
    public Mask SwordMask;

    public float DamageMin = 5f, DamageMax = 10f;
    public int AttackCooldown = 500, AttackTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (AttackTime == 0)
        {
            if (Vector2.Distance(player.transform.position, enemy.transform.position) < AttackRange)
            {
                PlayerMovement.MinusHealth(Random.Range(DamageMin, DamageMax));
                AttackTime = AttackCooldown;
            }
        }
        else AttackTime--;
        if (AttackTime < 0) AttackTime = 0;
    }
}
