using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FXDamageBubbleGeneral : MonoBehaviour
{
    public float Damage;
    public Camera Cam;
    public List<float> FloatAnimation;
    int ShelfLife = 100;
    int AnimationIndex = 0;

    void Awake()
    {
        Cam = FindObjectOfType<Camera>();
        GetComponentInChildren<Canvas>().worldCamera = Cam;
        GetComponent<FXDamageBubbleGeneral>().enabled = true;
        for(float i = 200;i>=0.02;i/=2)
        {
            FloatAnimation.Add(i/200);
        }
    }
    void Update()
    {
        GetComponentInChildren<Text>().text = Math.Round(Damage,2).ToString();
        transform.position = new Vector3(transform.position.x, transform.position.y+(FloatAnimation[AnimationIndex]), transform.position.z);
        if(ShelfLife>0) ShelfLife--;
        else Destroy(gameObject);
        if (AnimationIndex < FloatAnimation.Count-1) AnimationIndex++;
    }
}
