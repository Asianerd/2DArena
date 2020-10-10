using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public List<GameObject> RangeProjectilePrefabList;
    public List<GameObject> ProjectilePrefabList;
    public List<Sprite> WeaponSpriteList;

    public GameObject GenericRangeProjectile;
    public GameObject GenericProjectile;

    public List<Sprite> RangeWeaponProjectileList;
    public List<Sprite> ProjectileSpriteList;


    public class Weapon
    {
        public string WeaponName;
        public float DamageMin, DamageMax, Knockback;
        public int WeaponCooldown, Rarity, Durability, MaxDurability;
        public bool IsBreakable;
        public int ManaUsage;
        public int Category;
        public int WeaponID;
        public int[][] CategoryReference = new int[][] { new int[] { 0, 1, 2, 3, 4 }, new int[] { 0, 1 }, new int[] { 0, 1, 2, 3 } };
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
        public Weapon(string name, float DmgMin,float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnCategory, int WpnType, int WpnID,
            float WpnRange,float WpnWidth
            , int WpnMaxDurability = -10, int WpnEffect = 0, int WpnMana= 0 )
        {
            // Default values for every weapon
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -10)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;

            Rarity = WpnRarity;
            Category = WpnCategory;
            Type = WpnType;
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;

            // Melee
            AttackRange = WpnRange;
            WeaponWidth = WpnWidth;
        }

        // Range
        public Weapon(string name, float DmgMin,float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnCategory,int WpnType, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnMaxDurability = -10, int WpnEffect = 0,int WpnMana=0)
        {
            // Default values for every weapon
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -10)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;

            Rarity = WpnRarity;
            Category = WpnCategory;
            Type = WpnType;
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;

            // Range
            RangeProjectile = WpnProjectile;
            ShelfLife = WpnShelfLife;
            ProjectileSpeed= WpnProjectileSpeed;
            ProjectileSpriteID = WpnProjectileSpriteID;
        }

        // Projectile
        public Weapon(string name, float DmgMin,float DmgMax, float WpnKnockback, int WpnCooldown, int WpnRarity, int WpnCategory, int WpnType, int WpnID,
            int WpnShelfLife, GameObject WpnProjectile, int WpnAmount, float WpnProjectileSpeed, int WpnProjectileSpriteID
            , int WpnMaxDurability = -10, int WpnEffect = 0,int WpnMana=0,bool WpnProjectileSpin = false,float WpnProjectileSpinSpeed = 0)
        {
            // Default values for every weapon
            WeaponName = name;
            DamageMin = DmgMin;
            DamageMax = DmgMax;
            Knockback = WpnKnockback;
            WeaponCooldown = WpnCooldown;

            if (WpnMaxDurability == -10)
                IsBreakable = false;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;

            Rarity = WpnRarity;
            Category = WpnCategory;
            Type = WpnType;
            Effect = WpnEffect;
            ManaUsage = WpnMana;
            WeaponID = WpnID;

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
    }

    public static List<Weapon> GlobalWeaponList = new List<Weapon>();

    void Awake()
    {
        void m(string Name, float dmgmin,float dmgmax, float wpnknock, int cooldown, int rarity, int category, int type, int weaponid,float range,float width,int maxdurability = -10, int fx = 0,int mana=0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity, category, type, weaponid, range, width, maxdurability, fx, mana));
        }
        void r(string Name, float dmgmin,float dmgmax, float wpnknock, int cooldown, int rarity, int category, int type, int weaponid,int shelflife, GameObject projectile,float speed, int spriteid,int maxdurability = -10, int fx =0,int mana=0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity, category, type, weaponid, shelflife, projectile, speed, spriteid, maxdurability, fx, mana));
        }
        void p(string Name, float dmgmin,float dmgmax, float wpnknock, int cooldown, int rarity, int category, int type, int weaponid,int shelflife, GameObject projectile,float speed, int amount,int spriteid,int maxdurability = -10, int fx = 0,int mana=0,bool spin=false,float spinspeed=0)
        {
            GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, cooldown, rarity, category, type, weaponid, shelflife, projectile, amount, speed, spriteid, maxdurability, fx, mana,spin,spinspeed));
        }
        //Name Min Max Knck Cool Rare Cate Type Sprite
        m("Aluminium shortsword", 2, 3, 4f, 100, 0, 0, 0, 1, 1.5f, 0.05f, 60);
        m("Silicon shortsword", 3, 4, 2f, 100, 0, 0, 0, 2, 10, 0.05f);
        m("Iron shortsword", 5, 6, 2, 100, 0, 0, 0, 3, 5, 0.05f);

        r("Bow", 5, 10, 1, 1, 1, 1, 0, 4, 500, GenericRangeProjectile, 0.1f, 1);
        r("Darts", 50, 100, 0, 0, 0, 1, 0, 5, 500, GenericRangeProjectile, 0.5f, 0);

        p("Throwing Knives", 1, 5, 1f, 20, 0, 2, 0, 0, 100, GenericProjectile, 0.5f, 5, 0);
        p("Throwing Stars", 1, 10, 1f, 5, 0, 2, 1, 0, 100, GenericProjectile, 0.5f, 20, 1,spin:true,spinspeed:5);
    }

}
