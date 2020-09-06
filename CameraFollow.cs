using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; //Fetched in Start()
    public float CamMove = 0.05f, CamRange = 1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + (Input.GetAxis("Horizontal")/CamRange), player.transform.position.y + (Input.GetAxis("Vertical") / CamRange), -10f), CamMove);   
    }
}
