using System;
using System.Collections.Generic;
using UnityEngine;

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
        public int WeaponID;
        public int ScriptID;
        public int Level, LevelCurrentProgression, LevelNextLevelProgression;
        /* Category
         * 
         * (0) Melee
         * (1) Range
         * (2) Projectile
         * 
         */
        public int Type;
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
        public float AttackRange;
        public float WeaponWidth;
        // Range
        public GameObject RangeProjectile;
        // Projectile
        public GameObject Projectile;
        public int Used;
        public int Amount;
        public bool ProjectileSpin;
        public float ProjectileSpinSpeed;
        // Gun
        
        // Range & Projectile
        public int ShelfLife;
        public float ProjectileSpeed;
        public int ProjectileSpriteID;
        /* Overload order
         * 
         * Melee
         * Range
         * Projectile
         */
        // Melee
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel, int WpnCategory, int WpnType, int WpnID,
            float WpnRange, float WpnWidth
            , int WpnCProgress = 0, int WpnMaxDurability = -100, int WpnEffect = 0, int WpnMana = 0, int WpnScriptID = 0)
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

            Type = WpnType;
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;
            ScriptID = WpnScriptID;


            // Melee
            AttackRange = WpnRange;
            WeaponWidth = WpnWidth;
        }

        // Range
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnType, int WpnID,
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
            Type = WpnType;
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
        public Weapon(string name, float DmgMin, float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnLevel,int WpnCategory, int WpnType, int WpnID,
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
            Type = WpnType;
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

        // Weapon = new Weapon(Weapon); - just ignore this
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
            Type = weapon.Type;
            Effect = weapon.Effect;
            ManaUsage = weapon.ManaUsage;
            WeaponID = weapon.WeaponID;
            ScriptID = weapon.ScriptID;


            // Melee
            AttackRange = weapon.AttackRange;
            WeaponWidth = weapon.WeaponWidth;

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
        void m(string Name, float dmgmin, float dmgmax, float wpnknock, int cooldown, int rarity, int lvl,int category, int type, int weaponid,float range,float width,int lvlcprogress=0,int maxdurability = -100, int fx = 0,int mana=0,int scriptid=0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity, lvl,category, type, weaponid, range, width, lvlcprogress, maxdurability, fx, mana, scriptid));
        }
        void r(string Name, float dmgmin, float dmgmax, float wpnknock, int cooldown, int rarity, int lvl,int category, int type, int weaponid, int shelflife, GameObject projectile, float speed, int spriteid, int lvlcprogress=0,int maxdurability = -100, int fx = 0, int mana = 0, int scriptid = 0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity, lvl,category, type, weaponid, shelflife, projectile, speed, spriteid ,lvlcprogress,maxdurability, fx, mana, scriptid));
        }
        void p(string Name, float dmgmin, float dmgmax, float wpnknock, int cooldown, int rarity, int lvl,int category, int type, int weaponid, int shelflife, GameObject projectile, float speed, int amount, int spriteid, int lvlcprogress=0,int maxdurability = -100, int fx = 0, int mana = 0, bool spin = false, float spinspeed = 0, int scriptid = 0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity,lvl, category, type, weaponid, shelflife, projectile, amount, speed, spriteid, lvlcprogress,maxdurability, fx, mana,spin,spinspeed, scriptid));
        }


        m("Aluminium shortsword",   dmgmin: 2, dmgmax: 3, wpnknock: 4f, cooldown: 100, rarity: 0,1, category: 0, type: 0, weaponid: 1, range: 1.5f, width: 0.05f, maxdurability: 60);
        m("Silicon shortsword",     dmgmin: 3, dmgmax: 4, wpnknock: 2f, cooldown: 100, rarity: 1, 1, category: 0, type: 0, weaponid: 2, range: 10, width: 0.05f);
        m("Iron shortsword",        dmgmin: 5, dmgmax: 6, wpnknock: 2, cooldown: 100, rarity: 2, 1, category: 0, type: 0, weaponid: 3, range: 5, width: 0.05f);
        m("Spear object",           dmgmin: 10, dmgmax: 20, wpnknock: 0.5f, cooldown: 100, rarity: 3, 1, category: 0, type: 0, weaponid: 4, range: 5, width: 1,    scriptid: 1);
        m("Auto-aim",               dmgmin: 10, dmgmax: 20, wpnknock: 2, cooldown: 100, rarity: 4, 1, category: 0, type: 0, weaponid: 7, range: 5, width: 3,       scriptid: 2);

        r("Bow", 5, 10, 1, 1, 1, 1, 1, 0, 0, 500, GenericRangeProjectile, 0.1f, 1);
        r("Scar H", 50, 100, 0, 0, 5, 1, 1, 0, 1, 500, GenericRangeProjectile, 0.5f, 0);
        r("M16", 20, 40, 0, 5, 0, 1, 1, 0, 2, 500, GenericRangeProjectile, 0.5f, 2);

        p("Throwing Knives", 1, 5, 1f, 20, 0, 1, 2, 0, 0, 100, GenericProjectile, 0.5f, 5, 0);
        p("Throwing Stars", 1, 10, 1f, 5, 0, 1, 2, 1, 1, 500, GenericProjectile, 0.5f, 20, 1, spin: true, spinspeed: 5);
        p("Spear", 10, 20, 0.5f, 100, 0, 1, 2, 0, 2, 200, GenericProjectile, 0.2f, 3, 2);
    }
}
