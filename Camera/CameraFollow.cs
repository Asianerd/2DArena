using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player; //Fetched in Start()
    public float camRange = 1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 Target = new Vector3(player.transform.position.x + (Input.GetAxis("Horizontal") / camRange), player.transform.position.y + (Input.GetAxis("Vertical") / camRange));
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), Target, 0.125f);
    }
}
