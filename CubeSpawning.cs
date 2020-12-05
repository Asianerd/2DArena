using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawning : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyTemplate;
    public GameObject runtimeScript;
    public float randXMin, randXMax, randYMin, randYMax;
    public int cooldown = 400;
    int i = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        runtimeScript = GameObject.FindGameObjectWithTag("RuntimeScript");
    }

    void Update()
    {
        if (!InventoryGeneral.gamePaused)
        {
            if ((i == 0) | (Input.GetKeyDown(KeyCode.E)))
            {
                Instantiate(enemyTemplate, new Vector2(Random.Range(randXMin, randXMax), Random.Range(randYMin, randYMax)), Quaternion.identity);
            }
            if (i > cooldown) i = 0;
            else i++;
        }
    }
}
