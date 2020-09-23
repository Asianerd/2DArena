using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player; //Rigidbody2D - Cant be fetched by GameObject.FindObjectWithTag()
    public float MovementSpeed = 4f;
    public float SprintMultiplier = 2f;
    float XMove,YMove;
    Vector2 movement = new Vector2();
    bool IsSprint = false;

    void Update()
    {
        XMove = Input.GetAxisRaw("Horizontal");
        YMove = Input.GetAxisRaw("Vertical");
        IsSprint = Input.GetButton("Sprint");
        if (IsSprint) movement = new Vector2(XMove * MovementSpeed*SprintMultiplier, YMove * MovementSpeed*SprintMultiplier);
        else movement = new Vector2(XMove * MovementSpeed, YMove * MovementSpeed);
        player.velocity = movement;
    }
}
