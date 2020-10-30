using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyesFollowScript : MonoBehaviour
{
    public float EyeDistance = 0.1f;
    void Update()
    {
        if(!InventoryGeneral.GamePaused)
            transform.position = new Vector2(PlayerGeneral.PlayerPosition.x + (Mathf.Cos(PlayerGeneral.MouseAngle) * EyeDistance), PlayerGeneral.PlayerPosition.y + (Mathf.Sin(PlayerGeneral.MouseAngle) * EyeDistance));
    }
}
