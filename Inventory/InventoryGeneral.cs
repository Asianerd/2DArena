using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGeneral : MonoBehaviour
{
    public GameObject player;
    public GameObject inventoryUI;
    public GameObject runtime;
    public GameObject cursorObject;

    public GameObject[] inventories;
    public static int inventoryOpen = -1;
    /*
    -1 None
    0 Character
    1 Weapon
    2 Equipment(Equipped)
    3 Equipment storage
    4 Skills(Equipped)
    5 Skill storage*/

    public static bool gamePaused = false;

    // on runtime

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryUI.SetActive(false);
        cursorObject = GameObject.FindGameObjectWithTag("CursorObject");
        inventories = Resources.LoadAll<GameObject>("Prefabs/Inventory Objects");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            cursorObject.GetComponent<CursorSpriteScript>().UpdateCursorSprite();
            if (gamePaused)
            {
                inventoryUI.SetActive(true);
                OpenInventory(1);
                Time.timeScale = 0f;
            }
            else
            {
                inventoryUI.SetActive(false);
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
        inventoryOpen = InventoryType;
        Instantiate(inventories[InventoryType],inventoryUI.transform);
    }

    public void CloseInventory()
    {
        inventoryOpen = -1;
        foreach(Transform child in inventoryUI.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
