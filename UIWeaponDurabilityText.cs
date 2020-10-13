using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponDurabilityText : MonoBehaviour
{
    public string DurabilityText;

    void Update()
    {
        if (PlayerGeneral.CurrentWeaponReference.MaxDurability != -100)
            DurabilityText = $"{PlayerGeneral.CurrentWeaponReference.Durability}/{PlayerGeneral.CurrentWeaponReference.MaxDurability}";
        else
            DurabilityText = "";
        GetComponentInChildren<Text>().text = DurabilityText;
    }
}
