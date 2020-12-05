using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FXDamageBubbleGeneral : MonoBehaviour
{
    public float damage;
    public Camera cam;
    public List<float> floatAnimation;
    int shelfLife = 100;
    int animationIndex = 0;

    void Awake()
    {
        cam = FindObjectOfType<Camera>();
        GetComponentInChildren<Canvas>().worldCamera = cam;
        GetComponent<FXDamageBubbleGeneral>().enabled = true;
        for (float i = 200; i >= 0.02; i /= 2)
        {
            floatAnimation.Add(i / 200);
        }
    }
    void Update()
    {
        GetComponentInChildren<Text>().text = Math.Round(damage, 2).ToString();
        transform.position = new Vector3(transform.position.x, transform.position.y + (floatAnimation[animationIndex]), transform.position.z);
        if (shelfLife > 0) shelfLife--;
        else Destroy(gameObject);
        if (animationIndex < floatAnimation.Count - 1) animationIndex++;
    }
}
