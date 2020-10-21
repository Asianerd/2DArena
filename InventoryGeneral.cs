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
    public bool GamePaused = false;
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InventoryUI.SetActive(false);
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
            }
            else
            {
                UpdateInventory();
                Time.timeScale = 0f;
                GamePaused = true;
                InventoryUI.SetActive(true);
            }
        }
    }

    public void UpdateInventory()
    {
        foreach(WeaponData.Weapon i in PlayerGeneral.InventoryWeapon)
        {
            GameObject obj = Instantiate(ButtonPrefab,ContentObject.transform);
            obj.GetComponentInChildren<Text>().text = i.WeaponName;
            obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(i);
            switch (i.Category)
            {
                case 1:
                    obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().RangeWeaponSpriteList[i.WeaponID];
                    break;
                case 2:
                    obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().ProjectileSpriteList[i.WeaponID];
                    break;
                default:
                    obj.GetComponent<Button>().image.sprite = Runtime.GetComponent<WeaponData>().MeleeWeaponSpriteList[i.WeaponID];
                    break;
            }
        }
    }
}
