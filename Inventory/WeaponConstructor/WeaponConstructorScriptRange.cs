using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstructorScriptRange : MonoBehaviour
{
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

    // Range
    public GameObject ProjectileFired;
    public float ProjectileSpeed;
    public int ProjectileSpriteID;
    public int ShelfLife;
}
