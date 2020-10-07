using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawning : MonoBehaviour
{
    public GameObject Player;
    public GameObject EnemyTemplate;
    public Sprite EnemySprite;
    public GameObject RuntimeScript;
    public float RandXMin, RandXMax, RandYMin, RandYMax;
    public int cooldown = 400;
    int i = 0;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        RuntimeScript = GameObject.FindGameObjectWithTag("RuntimeScript");
    }

    void Update()
    {
        if (!InventoryShow.GamePaused)
        {
            if ((i == 0) | (Input.GetKeyDown(KeyCode.E)))
            {
                Instantiate(EnemyTemplate, new Vector2(Random.Range(RandXMin, RandXMax), Random.Range(RandYMin, RandYMax)), Quaternion.identity);
            }
            if (i > cooldown) i = 0;
            else i++;
        }
    }
}
