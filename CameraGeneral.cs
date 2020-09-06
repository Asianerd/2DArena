using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGeneral : MonoBehaviour
{
    public GameObject Player; //Fetched in Start()
    public float ShakeIntensity;
    public int i = 0,NudgeLength = 50;


    void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() 
    {
        if (i != 0)
        {
            transform.position = new Vector3(transform.position.x+Random.Range(-ShakeIntensity,ShakeIntensity),transform.position.y+Random.Range(-ShakeIntensity,ShakeIntensity),-10f);
            i--;
        }
    }
    public void CamShake(float Intensity, int Length)
    {
        ShakeIntensity = Intensity;
        NudgeLength = Length;
        i = NudgeLength;
    }
}
