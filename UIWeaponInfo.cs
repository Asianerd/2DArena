using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
    public string DurabilityText;
    int temp;

    void Update()
    {
        if (temp > 10)
        {
            switch (PlayerGeneral.CurrentWeaponReference.Category)
            {
                case 2:
                    DurabilityText = $"{PlayerGeneral.ProjectilesUsed}/{PlayerGeneral.CurrentWeaponReference.Amount}";
                    break;
                default:
                    if (PlayerGeneral.CurrentWeaponReference.IsBreakable)
                        DurabilityText = $"{PlayerGeneral.CurrentWeaponReference.Durability}/{PlayerGeneral.CurrentWeaponReference.MaxDurability}";
                    else
                    {
                        DurabilityText = "";
                    }
                    break;
            }
            GetComponentInChildren<Text>().text = DurabilityText;
        }
        else temp++;
    }
}
