using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventoryGeneral : MonoBehaviour
{
    public GameObject contentObject;
    public GameObject buttonPrefab;
    public GameObject weaponSelectTab;
    public GameObject selectionOutline;


    void OnEnable()
    {
        // When the inventory is opened
        foreach (Transform child in contentObject.transform) {Destroy(child.gameObject);}
        UpdateInventory();
    }

    public void UpdateInventory(WeaponData.Weapon SelectedWeapon)
    {
        // This is when the inventory is already opened

        // The current selected weapon (the one shown in the selection tab) is passed; All weapons from the players inventory gets instantiated but if the weapon
        // is the selected weapon, the selectiontab will then show its stats.

        // This is done because when a change is applied to the inventory, the whole inventory needs to be Destroy()ed and Instantiate()ed again to update the changes.
        // That will lose the LButton field for the InventorySelectionTabGeneral.cs. This will then keep the weapon selected and redeclare the LButton field.
        foreach (WeaponData.Weapon i in PlayerGeneral.inventoryWeapon)
        {
            GameObject obj = Instantiate(buttonPrefab, contentObject.transform);
            obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(i);
            if (i == SelectedWeapon)
            {
                weaponSelectTab.GetComponent<InventorySelectionTabGeneral>().Set(i, obj);
            }
        }
    }

    public void UpdateInventory()
    {
        // This is when inventory is opened
        List<WeaponData.Weapon> PlayerInventory;
        PlayerInventory = new List<WeaponData.Weapon>(PlayerGeneral.inventoryWeapon);

        WeaponData.Weapon FirstWeapon;
        List<WeaponData.Weapon> OtherWeapons;

        if (PlayerInventory.Count >= 1) FirstWeapon = PlayerInventory[0];
        else
        {
            FirstWeapon = null;
            weaponSelectTab.GetComponent<InventorySelectionTabGeneral>().SetEmpty();
        }
        if (PlayerInventory.Count > 1)
        {
            OtherWeapons = PlayerInventory;
            OtherWeapons.RemoveAt(0);
        }
        else OtherWeapons = null;



        GameObject obj;
        if (FirstWeapon != null)
        {
            obj = Instantiate(buttonPrefab, contentObject.transform);
            obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(FirstWeapon);
            weaponSelectTab.GetComponent<InventorySelectionTabGeneral>().Set(FirstWeapon, obj);
        }

        if (OtherWeapons != null)
        {
            foreach (WeaponData.Weapon i in OtherWeapons)
            {
                obj = Instantiate(buttonPrefab, contentObject.transform);
                obj.GetComponent<InventoryWeaponButtonGeneral>().SetWeapon(i);
            }
        }
    }
}
