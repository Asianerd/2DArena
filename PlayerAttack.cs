using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] EnemyArray;
    public GameObject Player;
    public float AttackRange = 1f;
    public float PlayerDamage = 10f;
    public float PlayerWeaponDamage;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        PlayerWeaponDamage = UnityEngine.Random.Range(GetComponent<PlayerGeneral>().EquippedWeapon.DamageMin, GetComponent<PlayerGeneral>().EquippedWeapon.DamageMax);
        EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        //EnemyArray[0].GetComponent<EnemyGeneral>().MinusHealth(10);
        foreach(GameObject i in EnemyArray)
        {
            if ((i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) < AttackRange) && (Input.GetMouseButtonDown(0)))
            {
                i.GetComponent<EnemyGeneral>().MinusHealth(PlayerWeaponDamage+PlayerDamage);
            }
        }
    }
}
