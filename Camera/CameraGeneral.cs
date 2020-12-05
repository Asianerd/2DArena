using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CameraGeneral : MonoBehaviour
{
    public GameObject player;
    public float shakeIntensity;
    public int i = 0,nudgeLength = 50;
    public float wantedZoom;
    public float zoomFactor = 3;
    public float zoomLerpSpeed = 10;
    Camera playerCam;


    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = Camera.main;
        wantedZoom = playerCam.orthographicSize;
    }

    void Update() 
    {
        if (!InventoryGeneral.gamePaused)
        {
            float Scroll;
            Scroll = Input.GetAxis("Mouse ScrollWheel");
            wantedZoom -= Scroll * zoomFactor;
            playerCam.orthographicSize = Mathf.Lerp(playerCam.orthographicSize, wantedZoom, Time.deltaTime * zoomLerpSpeed);
            wantedZoom = Mathf.Clamp(wantedZoom, 4.5f, 16f);
            if (i != 0)
            {
                transform.position = new Vector3(transform.position.x + Random.Range(-shakeIntensity, shakeIntensity), transform.position.y + Random.Range(-shakeIntensity, shakeIntensity), -10f);
                i--;
            }
        }
    }
    public void CamShake(float Intensity, int Length)
    {
        shakeIntensity = Intensity;
        nudgeLength = Length;
        i = nudgeLength;
    }
}
