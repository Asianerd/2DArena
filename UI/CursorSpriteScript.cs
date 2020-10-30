using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorSpriteScript : MonoBehaviour
{
    public static Sprite[] CursorSpriteList;

    void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
    }

    void Awake()
    {
        Cursor.visible = false;
        CursorSpriteList = Resources.LoadAll<Sprite>("CursorSprites");
        StartCoroutine(EWait());
    }
    IEnumerator EWait()
    {
        yield return new WaitForSeconds(1);
        UpdateCursorSprite();
    }

    public void UpdateCursorSprite()
    {
        if(InventoryGeneral.GamePaused)
            GetComponent<Image>().sprite = CursorSpriteList[0];
        else
            switch (PlayerGeneral.CurrentWeaponReference.Category)
            {
                case 0:
                    GetComponent<Image>().sprite = CursorSpriteList[1];
                    break;
                case 1:
                    GetComponent<Image>().sprite = CursorSpriteList[2];
                    break;
                case 2:
                    GetComponent<Image>().sprite = CursorSpriteList[3];
                    break;
                default:
                    GetComponent<Image>().sprite = CursorSpriteList[0];
                    break;
            }
    }
}
