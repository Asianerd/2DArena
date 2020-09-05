using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player;
    public float MovementSpeed = 4f;
    public float SprintMultiplier = 2f;
    float XMove,YMove;
    Vector2 movement = new Vector2();
    bool IsSprint = false;

    public static float health = 10000f;
    public float PrevHealth, CurHealth = 0, LostHealth = 0;


    public Camera Cam;
    public float DefCamFov = 100f;

    // Update is called once per frame
    void Update()
    {
        XMove = Input.GetAxisRaw("Horizontal");
        YMove = Input.GetAxisRaw("Vertical");
        IsSprint = Input.GetButton("Sprint");
        if (IsSprint) movement = new Vector2(XMove * MovementSpeed*SprintMultiplier, YMove * MovementSpeed*SprintMultiplier);
        else movement = new Vector2(XMove * MovementSpeed, YMove * MovementSpeed);
        player.velocity = movement;

        PrevHealth = CurHealth;
        CurHealth = health;
        LostHealth = PrevHealth - CurHealth;
        if (LostHealth < 10f) Cam.fieldOfView -= LostHealth;
        else Cam.fieldOfView -= 10f;
        Debug.Log("Lost Health : "+LostHealth);
        Debug.Log("FOV : " + Cam.fieldOfView);
        Debug.Log("Previous Health : "+PrevHealth);
        Debug.Log("Current Health : "+CurHealth);

        if (Cam.fieldOfView != DefCamFov)
        {
            if ((Cam.fieldOfView > DefCamFov ) && (DefCamFov > Cam.fieldOfView + 0.01f))
                Cam.fieldOfView = DefCamFov;
            else if (Cam.fieldOfView < DefCamFov)
                Cam.fieldOfView = Cam.fieldOfView + 0.01f;
            if ((Cam.fieldOfView < DefCamFov) && (DefCamFov < Cam.fieldOfView - 0.01f))
                Cam.fieldOfView = DefCamFov;
            else if (Cam.fieldOfView > DefCamFov)
                Cam.fieldOfView = Cam.fieldOfView - 0.01f;
        }

    }
    static public void MinusHealth(float losthealth)
    {
        health -= losthealth;
    }
}
