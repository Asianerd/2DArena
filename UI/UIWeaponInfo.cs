using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
    public string DurabilityText;
    Text TextReference;
    bool Buffer = false;


    
    void Update()
    {
        if(Buffer)
            TextUpdate();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Buffer = true;
    }
    void TextUpdate()
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
        TextReference.text = DurabilityText;
    }
    void Awake()
    {
        TextReference = GetComponentInChildren<Text>();
        StartCoroutine(Wait());
    }
}
