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

public class DiscardedWeaponData : MonoBehaviour
{
    public List<GameObject> RangeProjectilePrefabList;
    public List<GameObject> ProjectilePrefabList;

    public static Sprite[] GlobalMeleeWeaponSpriteList;
    public static Sprite[] GlobalRangeWeaponSpriteList;
    public static Sprite[] GlobalProjectileSpriteList;

    public GameObject GenericRangeProjectile;
    public GameObject GenericProjectile;

    public static Sprite[] GlobalRangeWeaponProjectileSpriteList;

    public List<Sprite> LootSpriteList;
    public GameObject LootPrefab;

    public static List<Weapon> GlobalWeaponList = new List<Weapon>();
    
    public static Sprite[] GlobalWeaponBorderList;
    public static Sprite[] GlobalWeaponTypeSprite;
    public static string[] GlobalWeaponRarityNames = new string[] { "Common", "Uncommon", "Rare", "Legendary", "Mythical", "Artifact" };
    public static Sprite[] GlobalRarityBackground;

    public class Weapon
    {
        public string WeaponName;
        public float DamageMin, DamageMax, Knockback;
        public int WeaponCooldown, Rarity, Durability, MaxDurability;
        public bool IsBreakable = true;
        public int ManaUsage;
        public int Category;
        public int WeaponID; // For the WeaponObject's Sprite
        public int ScriptID;
        public int Level, LevelCurrentProgression, LevelNextLevelProgression;
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
        public int Effect;
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
        public GameObject RangeProjectile;

            // Projectile
        public GameObject Projectile;
        public int Used;
        public int Amount;
        public bool ProjectileSpin;
        public float ProjectileSpinSpeed;
        
            // Range & Projectile
        public int ShelfLife;
        public float ProjectileSpeed;
        public int ProjectileSpriteID;     //Possibly removing these two as it might not be used (the sprites already in the projectile GameObject) DONT REMOVE - stupid fucking vs doesnt want to work

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
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;
            Level = WpnLevel;
            LevelCurrentProgression = WpnCProgress;
            LevelNextLevelProgression = 2^(WpnLevel+8);
            Rarity = WpnRarity;
            Category = WpnCategory;
            Level = WpnLevel;

            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;
            ScriptID = WpnScriptID;


            // Melee
        }

        // Range
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnCProgress=0, int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, int WpnScriptID = 0)
        {
            // Default values for every weapon
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;

            Rarity = WpnRarity;
            Category = WpnCategory;
            Level = WpnLevel;
            LevelCurrentProgression = WpnCProgress;
            LevelNextLevelProgression = Convert.ToInt32(Math.Pow(2,WpnLevel + 8));
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;
            ScriptID = WpnScriptID;


            // Range
            RangeProjectile = WpnProjectile;
            ShelfLife = WpnShelfLife;
            ProjectileSpeed= WpnProjectileSpeed;
            ProjectileSpriteID = WpnProjectileSpriteID;
        }

        // Projectile
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, int WpnAmount, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnCProgress=0,int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, bool WpnProjectileSpin = false, float WpnProjectileSpinSpeed = 0, int WpnScriptID = 0)
        {
            // Default values for every weapon
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -100)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;

            Rarity = WpnRarity;
            Category = WpnCategory;
            Level = WpnLevel;
            LevelCurrentProgression = WpnCProgress;
            LevelNextLevelProgression = 2 ^ (WpnLevel + 8);
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;
            ScriptID = WpnScriptID;

            // Projectile
            Projectile = WpnProjectile;
            ShelfLife = WpnShelfLife;
            Amount = WpnAmount;
            Used = 0;
            ProjectileSpeed = WpnProjectileSpeed;
            ProjectileSpriteID = WpnProjectileSpriteID;
            ProjectileSpin = WpnProjectileSpin;
            ProjectileSpinSpeed = WpnProjectileSpinSpeed;
        }


        // Weapon = new Weapon(Weapon); - just ignore this; done to make an instance without changing the reference; theres definitely a better way to do this
        public Weapon(Weapon weapon)
        {
            WeaponName = weapon.WeaponName;
            DamageMin = weapon.DamageMin;
            DamageMax = weapon.DamageMax;
            Knockback = weapon.Knockback;
            WeaponCooldown = weapon.WeaponCooldown;

            if (weapon.MaxDurability == -100)
                IsBreakable = false;
            MaxDurability = weapon.MaxDurability;
            Durability = weapon.Durability;

            Rarity = weapon.Rarity;
            Category = weapon.Category;
            Level = weapon.Level;
            LevelCurrentProgression = weapon.LevelCurrentProgression;
            LevelNextLevelProgression = weapon.LevelNextLevelProgression;
            Effect = weapon.Effect;
            ManaUsage = weapon.ManaUsage;
            WeaponID = weapon.WeaponID;
            ScriptID = weapon.ScriptID;


            // Melee

            // Range
            RangeProjectile = weapon.RangeProjectile;
            ShelfLife = weapon.ShelfLife;
            ProjectileSpeed = weapon.ProjectileSpeed;
            ProjectileSpriteID = weapon.ProjectileSpriteID;

            // Projectile
            Projectile = weapon.Projectile;
            ShelfLife = weapon.ShelfLife;
            Amount = weapon.Amount;
            Used = 0;
            ProjectileSpeed = weapon.ProjectileSpeed;
            ProjectileSpriteID = weapon.ProjectileSpriteID;
            ProjectileSpin = weapon.ProjectileSpin;
            ProjectileSpinSpeed = weapon.ProjectileSpinSpeed;
        }

        public void LevelCheck()
        {
            if(LevelCurrentProgression>=LevelNextLevelProgression)
            {
                LevelCurrentProgression -= LevelNextLevelProgression;
                Level++;
                LevelNextLevelProgression = Convert.ToInt32(Math.Pow(2, (Level + 8)));
                //LevelNextLevelProgression = 2 ^ (Level + 8); exponents dont work in c#  fuck
            }
        }
    }

    public class Loot
    {
        public string LootName;
        public int LootAmount,LootAmountMax;
        public Sprite LootSprite;
        public int LootID;
        public int PickupCooldown = 500;
        public int PickupCurrentCooldown = 500;
        public Loot(string Name, int Amount, int ID, Sprite SpriteID, int AmountMax = 999)
        {
            LootName = Name;
            LootID = ID;
            LootSprite = SpriteID;
            LootAmount = Amount;
            LootAmountMax = AmountMax;
        }
        public bool Pickable() { return (PickupCurrentCooldown == PickupCooldown); }
        public void PickupCountdown(bool Override = false)
        {
            if (PickupCurrentCooldown > 0 | Override)
                PickupCurrentCooldown--;
        }
    }

    void Awake()
    {
        GlobalWeaponBorderList = Resources.LoadAll<Sprite>("UISprites/Rarity/Border");
        GlobalRarityBackground = Resources.LoadAll<Sprite>("UISprites/Rarity/Selection");
        GlobalWeaponTypeSprite = Resources.LoadAll<Sprite>("UISprites/WeaponTypeSprites");
        GlobalMeleeWeaponSpriteList = Resources.LoadAll<Sprite>("WeaponSprites");
        GlobalRangeWeaponSpriteList = Resources.LoadAll<Sprite>("RangeProjectileWeaponSprites");
        GlobalRangeWeaponProjectileSpriteList = Resources.LoadAll<Sprite>("RangeProjectileSprites");
        GlobalProjectileSpriteList = Resources.LoadAll<Sprite>("ProjectileSprites");

        /* Creating all the weapons based on the fields in the WeaponConstructorScriptMelee/Range/Projectile scripts 
         * attached to the GameObjects in Assets/Resources/WeaponConstructor/(Melee/Range/Projectile) 
         * Yes, GetComponent is used. This is because this only occurs once; when starting the game and not every frame */
        GameObject[] WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Melee");
        foreach (GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptMelee x = y.GetComponent<WeaponConstructorScriptMelee>();
            GlobalWeaponList.Add(new Weapon(x.Name, x.DamageMin, x.DamageMax, x.Knockback, x.Cooldown, x.Rarity, x.Level, x.Category, x.WpnID, x.CProgress, x.MaxDurability, x.Effect, x.Mana, x.ScriptID));
        }

        WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Range");
        foreach(GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptRange x = y.GetComponent<WeaponConstructorScriptRange>();
            GlobalWeaponList.Add(new Weapon(x.Name, x.DamageMin, x.DamageMax, x.Knockback, x.Cooldown, x.Rarity, x.Level, x.Category, x.WpnID, x.ShelfLife, x.ProjectileFired, x.ProjectileSpeed, x.ProjectileSpriteID, x.CProgress, x.MaxDurability, x.Effect, x.Mana, x.ScriptID));
        }

        WeaponConstructorArray = Resources.LoadAll<GameObject>("WeaponConstructor/Projectile");
        foreach(GameObject y in WeaponConstructorArray)
        {
            WeaponConstructorScriptProjectile x = y.GetComponent<WeaponConstructorScriptProjectile>();
            GlobalWeaponList.Add(new Weapon(x.Name, x.DamageMin, x.DamageMax, x.Knockback, x.Cooldown, x.Rarity, x.Level, x.Category, x.WpnID, x.ShelfLife, x.ProjectileFired, x.Amount, x.ProjectileSpeed, x.ProjectileSpriteID, x.CProgress, x.MaxDurability, x.Effect, x.Mana, x.ProjectileSpin, x.ProjectileSpinSpeed, x.ScriptID));
        }


        /* foreach (Weapon x in GlobalWeaponList) // just to test if it works; it does
        { Debug.Log($"{x.Category} {x.WeaponName} : {x.DamageMin}/{x.DamageMax}"); } */
    }
}
