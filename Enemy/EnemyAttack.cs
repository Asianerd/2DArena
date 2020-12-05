using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject runtimeScript;

    public float attackRange = 3;
    public float damageMin = 5f, damageMax = 10f, damage;
    public int attackCooldown = 500, attackTime = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        runtimeScript = GameObject.FindGameObjectWithTag("RuntimeScript");
    }


    void Update()
    {
        if (!InventoryGeneral.gamePaused)
        {
            if (attackTime == 0)
            {
                if (Vector2.Distance(player.transform.position, transform.position) < (attackRange))
                {
                    damage = Random.Range(damageMin, damageMax);
                    player.GetComponent<PlayerGeneral>().MinusHealth(damage);
                    cam.GetComponent<CameraGeneral>().CamShake(damage / 100, 30);
                    attackTime = attackCooldown;
                }
            }
            else attackTime--;
            if (attackTime < 0) attackTime = 0;
        }
    }
}
