using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstructorScriptMelee : MonoBehaviour
{
    // On the WeaponConstructorObject
    public string Name;
    public float DamageMin, DamageMax;
    public float Knockback;
    public int Cooldown;

    public int Rarity;
    public int Category;
    public int Type;
    public int WpnID;

    public int Level = 1;

    public int CProgress = 0;
    public int MaxDurability = -100;
    public int Effect = 0;
    public int Mana = 0;
    public int ScriptID = 0;
}
