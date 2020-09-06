using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player; //Fetched in Start()
    public GameObject cam; //Fetched in Start()

    public float AttackRange = 3;
    public float DamageMin = 5f, DamageMax = 10f, Damage;
    public int AttackCooldown = 500, AttackTime = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }


    // Update is called once per frame
    void Update()
    {
        if (AttackTime == 0)
        {
            if (Vector2.Distance(player.transform.position, transform.position) < AttackRange)
            {
                Damage = Random.Range(DamageMin, DamageMax);
                player.GetComponent<PlayerGeneral>().MinusHealth(Damage);
                cam.GetComponent<CameraGeneral>().CamShake(Damage/100,30);
                AttackTime = AttackCooldown;
            }
        }
        else AttackTime--;
        if (AttackTime < 0) AttackTime = 0;
    }
}