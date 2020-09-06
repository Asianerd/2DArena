using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] EnemyArray;
    public float AttackRange = 1f;

    // Update is called once per frame
    void Update()
    {
        EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        //EnemyArray[0].GetComponent<EnemyGeneral>().MinusHealth(10);
        foreach(GameObject i in EnemyArray)
        {
            if ((i.GetComponent<EnemyGeneral>().DistanceEnemy(transform.position.x, transform.position.y) < AttackRange) && (Input.GetMouseButtonDown(0)))
            {
                i.GetComponent<EnemyGeneral>().MinusHealth(10f);
            }
        }
    }
}
