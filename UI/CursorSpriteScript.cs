using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorSpriteScript : MonoBehaviour
{
    public static Sprite[] cursorSpriteList;

    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
    }

    void Awake()
    {
        Cursor.visible = false;
        cursorSpriteList = Resources.LoadAll<Sprite>("CursorSprites");
        StartCoroutine(EWait());
    }
    IEnumerator EWait()
    {
        yield return new WaitForSeconds(1);
        UpdateCursorSprite();
    }

    public void UpdateCursorSprite()
    {
        if(InventoryGeneral.gamePaused)
            GetComponent<Image>().sprite = cursorSpriteList[0];
        else
            switch (PlayerGeneral.currentWeaponReference.category)
            {
                case 0:
                    GetComponent<Image>().sprite = cursorSpriteList[1];
                    break;
                case 1:
                    GetComponent<Image>().sprite = cursorSpriteList[2];
                    break;
                case 2:
                    GetComponent<Image>().sprite = cursorSpriteList[3];
                    break;
                default:
                    GetComponent<Image>().sprite = cursorSpriteList[0];
                    break;
            }
    }
}
