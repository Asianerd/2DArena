using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    public Text FPSText;
    float frameCount = 0;
    float dt = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;

    void Update()
    {
        frameCount++;
        dt += Time.deltaTime;
        if (dt > 1.0 / updateRate)
        {
            fps = frameCount / dt;
            frameCount = 0;
            dt -= 1.0f / updateRate;
        }
        FPSText.text = $"FPS : {fps}";
    }
}
