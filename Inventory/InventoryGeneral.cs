using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGeneral : MonoBehaviour
{
    public GameObject Player;
    public GameObject InventoryUI;
    public GameObject ContentObject;
    public GameObject ButtonPrefab;
    public GameObject Runtime;
    public GameObject CursorObject;
    public GameObject WeaponSelectTab;
    public GameObject SelectionOutline;
    public static bool GamePaused = false;

    public static GameObject CurrentWeaponGameObject;

    // on runtime

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InventoryUI.SetActive(false);
        CursorObject = GameObject.FindGameObjectWithTag("CursorObject");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                foreach(Transform child in ContentObject.transform)
                {
                    Destroy(child.gameObject);
                }
                Time.timeScale = 1f;
                GamePaused = false;
                InventoryUI.SetActive(false);
                CursorObject.GetComponent<CursorSpriteScript>().UpdateCursorSprite();
                WeaponSelectTab.GetComponent<InventorySelectionGeneral>().SelectionDefault();
            }
            else
            {
                UpdateInventory();
                Time.timeScale = 0f;
                GamePaused = true;
                InventoryUI.SetActive(true);
                CursorObject.GetComponent<CursorSpriteScript>().UpdateCursorSprite();
                WeaponSelectTab.GetComponent<InventorySelectionGeneral>().SelectionDefault();
            }
        }
    }
    IEnumerator DelayedSelectionMovement(GameObject Pos)
    {
        yield return new WaitForSeconds(0.1f);
        SelectionOutline.transform.position = Pos.transform.position;
        Debug.Log(Pos.transform.position.x);
    }

    public void UpdateInventory()
    {
        foreach(WeaponData.Weapon i in PlayerGeneral.InventoryWeapon)
        {
            GameObject obj = Instantiate(ButtonPrefab,ContentObject.transform);
            obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(i);
        }
    }
}
