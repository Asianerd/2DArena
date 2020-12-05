using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInfo : MonoBehaviour
{
    public string durabilityText;
    Text textReference;
    bool buffer = false;


    
    void Update()
    {
        if(buffer)
            TextUpdate();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        buffer = true;
    }
    void TextUpdate()
    {
        switch (PlayerGeneral.currentWeaponReference.category)
        {
            case 2:
                durabilityText = $"{PlayerGeneral.projectilesUsed}/{PlayerGeneral.currentWeaponReference.amount}";
                break;
            default:
                if (PlayerGeneral.currentWeaponReference.isBreakable)
                    durabilityText = $"{PlayerGeneral.currentWeaponReference.durability}/{PlayerGeneral.currentWeaponReference.maxDurability}";
                else
                {
                    durabilityText = "";
                }
                break;
        }
        textReference.text = durabilityText;
    }
    void Awake()
    {
        textReference = GetComponentInChildren<Text>();
        StartCoroutine(Wait());
    }
}
