using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WeaponData : MonoBehaviour
{
    public static Sprite[] globalMeleeWeaponSpriteList;
    public static Sprite[] globalRangeWeaponSpriteList;
    public static Sprite[] globalProjectileSpriteList;

    public GameObject genericRangeProjectile;
    public GameObject genericProjectile;

    public static Sprite[] globalRangeWeaponProjectileSpriteList;

    public GameObject lootPrefab;

    public static List<Weapon> globalWeaponList = new List<Weapon>();
    
    public static Sprite[] globalWeaponBorderList;
    public static Sprite[] globalWeaponTypeSprite;
    public static string[] globalWeaponRarityNames = new string[] { "Common", "Uncommon", "Rare", "Legendary", "Mythical", "Artifact" };
    public static Sprite[] globalRarityBackground;

    public class Weapon
    {
        public string weaponName;
        public float damageMin, damageMax, knockback;
        public int weaponCooldown, rarity, durability, maxDurability;
        public bool isBreakable = true;
        public int manaUsage;
        public int category;
        public int weaponID; // For the WeaponObject's Sprite
        public int scriptID;
        public int level, levelCurrentProgression, levelNextLevelProgression;
        /* Category
         * 
         * (0) Melee
         * (1) Range
         * (2) Projectile
         * 
         */
        /* Type
         * 
         * Melee
         *      (0) Shortsword
         *      (1) Broadsword
         *      (2) Dagger
         *      (3) Knife
         *      (4) Bat
         *      *Regular Melee weapons
         * 
         * Range
         *      (0) Bow
         *          Arrow
         *              - Checks if arrows are present in the inventory (only one arrow type please ffs)
         *          Magic
         *              - Spawns a magic based projectile
         *      (1) Staff
         *      *Spawns a projectile
         * 
         * Projectile
         *      (0) Throwing Knife
         *      (1) Throwing Star
         *      (2) Pellet
         *      (3) Spear
         *      *Will destroy GameObject and drop itself as WeaponLoot
         */
        /* Rarity
         * 
         * (0) Common - 50
         * (1) Uncommon - 30
         * (2) Rare - 10
         * (3) Legendary - 5
         * (4) Mythical - 4
         * (5) Artifact - 1
         * 
         */
        public int effect;
        /* Effects
         * 
         * (0) None
         * (1) Poison
         * (2) Burning
         * (3) Stun
         * (4) Slow
         * 
         */
        // Data for each Category
            // Melee

            // Range
        public GameObject rangeProjectile;

            // Projectile
        public GameObject projectile;
        public int used;
        public int amount;
        public bool projectileSpin;
        public float projectileSpinSpeed;
        
            // Range & Projectile
        public int shelfLife;
        public float projectileSpeed;
        public int projectileSpriteID;     //Possibly removing these two as it might not be used (the sprites already in the projectile GameObject) DONT REMOVE - stupid fucking vs doesnt want to work

        /* Overload order
         * 
         * Melee
         * Range
         * Projectile
         */
        // Melee
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel, int WpnCategory, int WpnID, 
            int WpnCProgress = 0, int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, int WpnScriptID = 0)
        {
            // Default values for every weapon
            weaponName = name;
            damageMin = DmgMin;
            damageMax = DmgMax;
            knockback = WpnKnockback;
            weaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                isBreakable = false;
            maxDurability = WpnMaxDurability;
            durability = maxDurability;
            level = WpnLevel;
            levelCurrentProgression = WpnCProgress;
            levelNextLevelProgression = 2^(WpnLevel+8);
            rarity = WpnRarity;
            category = WpnCategory;
            level = WpnLevel;

            effect = WpnEffect;
            manaUsage = WpnMana;
            weaponID = WpnID;
            scriptID = WpnScriptID;


            // Melee
        }

        // Range
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnCProgress=0, int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, int WpnScriptID = 0)
        {
            // Default values for every weapon
            weaponName = name;
            damageMin = DmgMin;
            damageMax = DmgMax;
            knockback = WpnKnockback;
            weaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                isBreakable = false;
            maxDurability = WpnMaxDurability;
            durability = maxDurability;

            rarity = WpnRarity;
            category = WpnCategory;
            level = WpnLevel;
            levelCurrentProgression = WpnCProgress;
            levelNextLevelProgression = Convert.ToInt32(Math.Pow(2,WpnLevel + 8));
            effect = WpnEffect;
            manaUsage = WpnMana;
            weaponID = WpnID;
            scriptID = WpnScriptID;


            // Range
            rangeProjectile = WpnProjectile;
            shelfLife = WpnShelfLife;
            projectileSpeed= WpnProjectileSpeed;
            projectileSpriteID = WpnProjectileSpriteID;
        }

        // Projectile
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, int WpnAmount, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnCProgress=0,int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, bool WpnProjectileSpin = false, float WpnProjectileSpinSpeed = 0, int WpnScriptID = 0)
        {
            // Default values for every weapon
            weaponName = name;
            damageMin = DmgMin;
            damageMax = DmgMax;
            knockback = WpnKnockback;
            weaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                isBreakable = false;
            maxDurability = WpnMaxDurability;
            durability = maxDurability;

            rarity = WpnRarity;
            category = WpnCategory;
            level = WpnLevel;
            levelCurrentProgression = WpnCProgress;
            levelNextLevelProgression = 2 ^ (WpnLevel + 8);
            effect = WpnEffect;
            manaUsage = WpnMana;
            weaponID = WpnID;
            scriptID = WpnScriptID;

            // Projectile
            projectile = WpnProjectile;
            shelfLife = WpnShelfLife;
            amount = WpnAmount;
            used = 0;
            projectileSpeed = WpnProjectileSpeed;
            projectileSpriteID = WpnProjectileSpriteID;
            projectileSpin = WpnProjectileSpin;
            projectileSpinSpeed = WpnProjectileSpinSpeed;
        }


        // Weapon = new Weapon(Weapon); - just ignore this; done to make an instance without changing the reference; theres definitely a better way to do this
        public Weapon(Weapon weapon)
        {
            weaponName = weapon.weaponName;
            damageMin = weapon.damageMin;
            damageMax = weapon.damageMax;
            knockback = weapon.knockback;
            weaponCooldown = weapon.weaponCooldown;

            if (weapon.maxDurability == -100)
                isBreakable = false;
            maxDurability = weapon.maxDurability;
            durability = weapon.durability;

            rarity = weapon.rarity;
            category = weapon.category;
            level = weapon.level;
            levelCurrentProgression = weapon.levelCurrentProgression;
            levelNextLevelProgression = weapon.levelNextLevelProgression;
            effect = weapon.effect;
            manaUsage = weapon.manaUsage;
            weaponID = weapon.weaponID;
            scriptID = weapon.scriptID;


            // Melee

            // Range
            rangeProjectile = weapon.rangeProjectile;
            shelfLife = weapon.shelfLife;
            projectileSpeed = weapon.projectileSpeed;
            projectileSpriteID = weapon.projectileSpriteID;

            // Projectile
            projectile = weapon.projectile;
            shelfLife = weapon.shelfLife;
            amount = weapon.amount;
            used = 0;
            projectileSpeed = weapon.projectileSpeed;
            projectileSpriteID = weapon.projectileSpriteID;
            projectileSpin = weapon.projectileSpin;
            projectileSpinSpeed = weapon.projectileSpinSpeed;
        }

        public void LevelCheck()
        {
            if(levelCurrentProgression>=levelNextLevelProgression)
            {
                levelCurrentProgression -= levelNextLevelProgression;
                level++;
                levelNextLevelProgression = Convert.ToInt32(Math.Pow(2, (level + 8)));
                //LevelNextLevelProgression = 2 ^ (Level + 8); exponents dont work in c#  fuck
            }
        }
    }

    public class Loot
    {
        public string lootName;
        public int lootAmount,lootAmountMax;
        public Sprite lootSprite;
        public int lootID;
        public int pickupCooldown = 500;
        public int pickupCurrentCooldown = 500;
        public Loot(string Name, int Amount, int ID, Sprite SpriteID, int AmountMax = 999)
        {
            lootName = Name;
            lootID = ID;
            lootSprite = SpriteID;
            lootAmount = Amount;
            lootAmountMax = AmountMax;
        }
        public bool Pickable() { return (pickupCurrentCooldown == pickupCooldown); }
        public void PickupCountdown(bool Override = false)
        {
            if (pickupCurrentCooldown > 0 | Override)
                pickupCurrentCooldown--;
        }
    }

    void Awake()
    {
        globalWeaponBorderList = Resources.LoadAll<Sprite>("UISprites/Rarity/Border");
        globalRarityBackground = Resources.LoadAll<Sprite>("UISprites/Rarity/Selection");
        globalWeaponTypeSprite = Resources.LoadAll<Sprite>("UISprites/WeaponTypeSprites");
        globalMeleeWeaponSpriteList = Resources.LoadAll<Sprite>("WeaponSprites");
        globalRangeWeaponSpriteList = Resources.LoadAll<Sprite>("RangeProjectileWeaponSprites");
        globalRangeWeaponProjectileSpriteList = Resources.LoadAll<Sprite>("RangeProjectileSprites");
        globalProjectileSpriteList = Resources.LoadAll<Sprite>("ProjectileSprites");

        /* Creating all the weapons based on the fields in the WeaponConstructorScriptMelee/Range/Projectile scripts 
         * attached to the GameObjects in Assets/Resources/WeaponConstructor/(Melee/Range/Projectile) 
         * Yes, GetComponent is used. This is because this only occurs once; when starting the game and not every frame */
        GameObject[] WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Melee");
        foreach (GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptMelee x = y.GetComponent<WeaponConstructorScriptMelee>();
            globalWeaponList.Add(new Weapon(x.name, x.damageMin, x.damageMax, x.knockback, x.cooldown, x.rarity, x.level, x.category, x.wpnID, x.cProgress, x.maxDurability, x.effect, x.mana, x.scriptID));
        }

        WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Range");
        foreach(GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptRange x = y.GetComponent<WeaponConstructorScriptRange>();
            globalWeaponList.Add(new Weapon(x.name, x.damageMin, x.damageMax, x.knockback, x.cooldown, x.rarity, x.level, x.category, x.wpnID, x.shelfLife, x.projectileFired, x.projectileSpeed, x.projectileSpriteID, x.cProgress, x.maxDurability, x.effect, x.mana, x.scriptID));
        }

        WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Projectile");
        foreach(GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptProjectile x = y.GetComponent<WeaponConstructorScriptProjectile>();
            globalWeaponList.Add(new Weapon(x.name, x.damageMin, x.damageMax, x.knockback, x.cooldown, x.rarity, x.level, x.category, x.wpnID, x.shelfLife, x.projectileFired, x.amount, x.projectileSpeed, x.projectileSpriteID, x.cProgress, x.maxDurability, x.effect, x.mana, x.projectileSpin, x.projectileSpinSpeed, x.scriptID));
        }


        /* foreach (Weapon x in GlobalWeaponList) // just to test if it works; it does
        { Debug.Log($"{x.Category} {x.WeaponName} : {x.DamageMin}/{x.DamageMax}"); } */
    }
}
