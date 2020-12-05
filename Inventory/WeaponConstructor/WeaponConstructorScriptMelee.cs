using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstructorScriptMelee : MonoBehaviour
{
    // On the WeaponConstructorObject
    public string name;
    public float damageMin, damageMax;
    public float knockback;
    public int cooldown;

    public int rarity;
    public int category;
    public int type;
    public int wpnID;

    public int level = 1;

    public int cProgress = 0;
    public int maxDurability = -100;
    public int effect = 0;
    public int mana = 0;
    public int scriptID = 0;
}
