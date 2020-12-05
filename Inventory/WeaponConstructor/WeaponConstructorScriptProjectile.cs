using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstructorScriptProjectile : MonoBehaviour
{
    // This weapon classed should be left unused
    // Range weapons can be used instead of this
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

    // Projectile
    public GameObject projectileFired;
    public float projectileSpeed; //
    public int projectileSpriteID; //
    public int shelfLife; //
    public int amount;

    public bool projectileSpin; // remove these as they are not used (only for the weapon selections tab and a bit more but those can refer the ProjectileFired GameObject instead)
    public float projectileSpinSpeed; //
}
