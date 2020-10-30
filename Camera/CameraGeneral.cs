using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CameraGeneral : MonoBehaviour
{
    public GameObject Player;
    public Camera PlayerCam;
    public float ShakeIntensity;
    public int i = 0,NudgeLength = 50;
    public float WantedZoom;
    public float ZoomFactor = 3;
    public float ZoomLerpSpeed = 10;


    void Start() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCam = Camera.main;
        WantedZoom = PlayerCam.orthographicSize;
    }

    void Update() 
    {
        if (!InventoryGeneral.GamePaused)
        {
            float Scroll;
            Scroll = Input.GetAxis("Mouse ScrollWheel");
            WantedZoom -= Scroll * ZoomFactor;
            PlayerCam.orthographicSize = Mathf.Lerp(PlayerCam.orthographicSize, WantedZoom, Time.deltaTime * ZoomLerpSpeed);
            WantedZoom = Mathf.Clamp(WantedZoom, 4.5f, 16f);
            if (i != 0)
            {
                transform.position = new Vector3(transform.position.x + Random.Range(-ShakeIntensity, ShakeIntensity), transform.position.y + Random.Range(-ShakeIntensity, ShakeIntensity), -10f);
                i--;
            }
        }
    }
    public void CamShake(float Intensity, int Length)
    {
        ShakeIntensity = Intensity;
        NudgeLength = Length;
        i = NudgeLength;
    }
}
