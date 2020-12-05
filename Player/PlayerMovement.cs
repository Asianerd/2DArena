using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player; //Rigidbody2D - Cant be fetched by GameObject.FindObjectWithTag()
    public float movementSpeed = 4f;
    public float sprintMultiplier = 2f;
    float xMove,yMove;
    Vector2 movement = new Vector2();
    bool isSprint = false;

    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        isSprint = Input.GetButton("Sprint");
        if (isSprint) movement = new Vector2(xMove * movementSpeed*sprintMultiplier, yMove * movementSpeed*sprintMultiplier);
        else movement = new Vector2(xMove * movementSpeed, yMove * movementSpeed);
        player.velocity = movement;
    }
}
