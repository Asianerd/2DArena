using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSpriteScript : MonoBehaviour
{
    public List<Sprite> CursorSpriteList;
    Camera MainCamera;

    private void Update()
    {
        Cursor.visible = false;
        transform.position = new Vector3(MainCamera.ScreenToWorldPoint(Input.mousePosition).x, MainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        switch(PlayerGeneral.CurrentWeaponReference.Category)
        {
            case 0:
                GetComponentInChildren<SpriteRenderer>().sprite = CursorSpriteList[1];
                break;
            case 1:
                GetComponentInChildren<SpriteRenderer>().sprite = CursorSpriteList[2];
                break;
            case 2:
                GetComponentInChildren<SpriteRenderer>().sprite = CursorSpriteList[3];
                break;
            default:
                GetComponentInChildren<SpriteRenderer>().sprite = CursorSpriteList[0];
                break;
        }
    }

    private void Start()
    {
        MainCamera = Camera.main;
    }
}
