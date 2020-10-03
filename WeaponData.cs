using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public class Weapon
    {
        public string WeaponName;
        public float DamageMin, DamageMax, WeaponRange, Knockback;
        public int WeaponCooldown,Rarity,Durability,MaxDurability;
        public bool IsBreakable;
        public int Category;
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
        public Weapon(string name, float DmgRangeMin, float DmgRangeMax, float WpnKnockback, float WpnRange, int WpnCooldown, int WpnRarity, int WpnMaxDurability,int WpnCategory,int WpnType,int WpnEffect = 0)
        {
            WeaponName = name;
            DamageMin = DmgRangeMin;
            DamageMax = DmgRangeMax;
            Knockback = WpnKnockback;
            WeaponRange = WpnRange;
            WeaponCooldown = WpnCooldown;
            MaxDurability = WpnMaxDurability;
            Durability = MaxDurability;
            Rarity = WpnRarity;

            Category = WpnCategory;
            Type = WpnType;

            Effect = WpnEffect;
        }
        public Weapon(string name, float DmgRangeMin, float DmgRangeMax, float WpnKnockback, float WpnRange, int WpnCooldown, int WpnRarity, bool WpnIsBreakable, int WpnCategory, int WpnType, int WpnEffect=0)
        {
            WeaponName = name;
            DamageMin = DmgRangeMin;
            DamageMax = DmgRangeMax;
            Knockback = WpnKnockback;
            WeaponRange = WpnRange;
            WeaponCooldown = WpnCooldown;
            MaxDurability = 1;
            Durability = MaxDurability;
            Rarity = WpnRarity;

            Category = WpnCategory;
            Type = WpnType;

            Effect = WpnEffect;

            IsBreakable = WpnIsBreakable;
        }
    }

    public static List<Weapon> GlobalWeaponList = new List<Weapon>();

    void Awake()
    {
        void Addw(string Name, float dmgmin, float dmgmax, float wpnknock, float wpnrange, int cooldown, int rarity, int category, int type, int maxdurability = -10,int fx = 0)
        {
            if (maxdurability == -10) GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, wpnrange, cooldown, rarity, false, category, type, fx));
            else GlobalWeaponList.Add(new Weapon(Name, dmgmin, dmgmax, wpnknock, wpnrange, cooldown, rarity, maxdurability, category, type, fx));
        }
        Addw("Aluminium shortsword", 3f, 4f, 1.5f, 1.5f, 100, 0, 0, 0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
