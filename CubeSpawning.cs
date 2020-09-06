using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawning : MonoBehaviour
{
    public GameObject Player; //Fetched in Start()
    public GameObject EnemyTemplate; //Init in Start()
    public Sprite EnemySprite;
    public float RandXMin, RandXMax, RandYMin, RandYMax;
    public int cooldown = 400;
    int i = 0;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //EnemyTemplate Init
        GameObject EnemyTemplate = new GameObject("Test", typeof(EnemyAttack), typeof(EnemyGeneral), typeof(EnemyPathfinding));
        SpriteRenderer renderer = EnemyTemplate.AddComponent<SpriteRenderer>();

        EnemyTemplate.AddComponent<Rigidbody2D>().freezeRotation = true;
        EnemyTemplate.GetComponent<Rigidbody2D>().gravityScale=0f;

        EnemyTemplate.AddComponent<BoxCollider2D>().size = new Vector2(1f,1f);
        EnemyTemplate.tag = "Enemy";
        renderer.sprite = EnemySprite;
    }

    void Update()
    {
        if (i == 1)
        {
            Instantiate(EnemyTemplate, new Vector2(Random.Range(RandXMin, RandXMax), Random.Range(RandYMin, RandYMax)), Quaternion.identity);
        }
        if (i > cooldown) i = 0;
        else i++;
    }
}
