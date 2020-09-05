using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawning : MonoBehaviour
{
    public GameObject obj;
    public float RandXMin, RandXMax, RandYMin, RandYMax;
    public int cooldown = 400;
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (i == 1) Instantiate(obj, new Vector2(Random.Range(RandXMin,RandXMax),Random.Range(RandYMin,RandYMax)), Quaternion.identity);
        if (i > cooldown) i = 0;
        else i++;
    }
}
