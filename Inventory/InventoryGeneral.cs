using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGeneral : MonoBehaviour
{
    public GameObject Player;
    public GameObject InventoryUI;
    public GameObject Runtime;
    public GameObject CursorObject;

    public GameObject[] Inventories;
    public static int InventoryOpen = -1;
    /*
    -1 None
    0 Character
    1 Weapon
    2 Equipment(Equipped)
    3 Equipment storage
    4 Skills(Equipped)
    5 Skill storage*/

    public static bool GamePaused = false;

    // on runtime

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InventoryUI.SetActive(false);
        CursorObject = GameObject.FindGameObjectWithTag("CursorObject");
        Inventories = Resources.LoadAll<GameObject>("Prefabs/Inventory Objects");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePaused = !GamePaused;
            CursorObject.GetComponent<CursorSpriteScript>().UpdateCursorSprite();
            if (GamePaused)
            {
                InventoryUI.SetActive(true);
                OpenInventory(2);
                Time.timeScale = 0f;
            }
            else
            {
                InventoryUI.SetActive(false);
                CloseInventory();
                Time.timeScale = 1f;
            }
        }
    }

    /*Inventory List
    0 Character
    1 Weapon
    2 Equipment (Equipped)
    3 Equipment storage
    4 Skills (Equipped)
    5 Skill storage
    */
    public void OpenInventory(int InventoryType)
    {
        InventoryOpen = InventoryType;
        Instantiate(Inventories[InventoryType],InventoryUI.transform);
    }

    public void CloseInventory()
    {
        InventoryOpen = -1;
        foreach(Transform child in InventoryUI.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
