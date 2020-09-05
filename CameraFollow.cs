using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float CamMove = 0.05f;
    // Update is called once per frame
    void Update()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(player.transform.position.x + (Input.GetAxis("Horizontal")), player.transform.position.y + (Input.GetAxis("Vertical")), -10f), CamMove);   
    }
}
