using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player; //Fetched in Start()
    public float CamRange = 1;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 Target = new Vector3(Player.transform.position.x + (Input.GetAxis("Horizontal") / CamRange), Player.transform.position.y + (Input.GetAxis("Vertical") / CamRange));
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), Target, 0.125f);
    }
}
