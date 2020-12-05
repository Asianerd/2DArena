using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyesFollowScript : MonoBehaviour
{
    public float eyeDistance = 0.1f;
    void Update()
    {
        if(!InventoryGeneral.gamePaused)
            transform.position = new Vector2(PlayerGeneral.playerPosition.x + (Mathf.Cos(PlayerGeneral.mouseAngle) * eyeDistance), PlayerGeneral.playerPosition.y + (Mathf.Sin(PlayerGeneral.mouseAngle) * eyeDistance));
    }
}
