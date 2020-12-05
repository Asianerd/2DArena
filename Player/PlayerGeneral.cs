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

public class PlayerGeneral : MonoBehaviour
{
    public GameObject runtimeScript;
    public GameObject damageBubblePrefab;
    public Transform projectileHolder;

    public Camera playerCamera;
    public static Vector3 mousePosition;
    public static float mouseAngle;
    public static Vector2 playerPosition;

    public GameObject wpnObject;
    public float cosmeticWeaponDistance = 1f;

    public static List<WeaponData.Weapon> inventoryWeapon = new List<WeaponData.Weapon>();
    public WeaponData.Weapon currentWeapon;
    public static WeaponData.Weapon currentWeaponReference;

    //HP
    public float HP = 100, HPMax = 100, HPRegen = 1;
    public int HPRegenTime = 100, HPRegenClock = 0;

    //ATK
    public float playerDamage = 3f;
    public int weaponCooldown = 0;

    //Projectile data
    public List<GameObject> projectileSpawned;
    public static int projectilesUsed;

    //Animation data
    public Animator playerEyeAnimator;
    public int playerEyeAnimationCooldown;
    int playerEyeCurrentAnimationCooldown = 0;



    public static bool weaponObjectIsFlipped = false;

    public GameObject tempText;

    public void ResetCurrentWeapon()
    {
        currentWeapon = new WeaponData.Weapon("Fists", 50, 100, 1, 100, 0, 1, 0, 0, 0);
    }

    void Awake()
    {
        ResetCurrentWeapon();
        playerCamera = FindObjectOfType<Camera>();
        runtimeScript = GameObject.FindGameObjectWithTag("RuntimeScript");
        playerEyeAnimationCooldown = UnityEngine.Random.Range(0, 10000);
    }

    void Update()
    {
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);

        mouseAngle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);

        playerPosition = new Vector2(transform.position.x, transform.position.y);

        currentWeaponReference = currentWeapon;

        tempText.GetComponent<Text>().text = $"LVL : {currentWeapon.level}  {currentWeapon.levelCurrentProgression}/{currentWeapon.levelNextLevelProgression}";

        // Remove please - taking 70ish fps to execute
        // Sets ProjectilesUsed to the amount of GameObjects with the same name
        // Just increase/decrease when a projectile is instantiated/destroyed
        // Projectile weapon class unused now
        /*
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Projectile");
        ProjectilesUsed = 0;
        foreach(GameObject gameobj in gameObjects)
        {
            if(gameobj.name == CurrentWeapon.WeaponName)
            {
                ProjectilesUsed = gameObjects.Count(n => n.name == CurrentWeapon.WeaponName);
                break;
            }
        }
        */

        currentWeapon.LevelCheck();

        // Debug keys
        if (Input.GetKey(KeyCode.C))
        {
            int SpawnedWeaponID = UnityEngine.Random.Range(0, WeaponData.globalWeaponList.Count);
            runtimeScript.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position, WeaponData.globalWeaponList[SpawnedWeaponID]);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.position = new Vector2(mousePosition.x, mousePosition.y);
        }
        // Debug keys

        if (!InventoryGeneral.gamePaused)
        {
            Regen();
            Attack();
            ShowWeapon();
            WeaponDurabilityCheck();
            PlayerEyeAnimation();
        }
    }

    public void MinusHealth(float losthealth)
    {
        HP -= losthealth;
    }

    public void MinusWeaponDurability(int LostDurability = 1,bool IsBreakOverride = false)
    {
        if (IsBreakOverride)
            currentWeapon.durability -= LostDurability;
        else
            if (currentWeapon.isBreakable && currentWeapon.durability > 0)
                currentWeapon.durability -= LostDurability;
    }

    public void AddWeaponLevelProgress(int WeaponProgress)
    {
        currentWeapon.levelCurrentProgression += WeaponProgress/10;
        currentWeapon.LevelCheck();
    }

    void WeaponDurabilityCheck()
    {
        if ((currentWeapon.durability <= 0) && !(currentWeapon.durability == -100))
        {
            ResetCurrentWeapon();
        }
    }

    void Attack()
    {
        if ((weaponCooldown == 0) && Input.GetMouseButton(0))
        {
            weaponCooldown = currentWeapon.weaponCooldown;
            switch (currentWeapon.category)
            {
                case 0:
                    MeleeAttack();
                    break;
                case 1:
                    RangeAttack();
                    break;
                case 2:
                    ProjectileAttack();
                    break;
                default:
                    break;
            }
        }
        if (weaponCooldown > 0) weaponCooldown--;
    }

    void MeleeAttack()
    {        
        wpnObject.GetComponent<WeaponObject>().SwingWrapper();
    }

    void RangeAttack()
    {
        float length = 1.4f;
        GameObject obj = Instantiate(currentWeapon.rangeProjectile, new Vector2(Convert.ToSingle((Mathf.Cos(mouseAngle)*length)+transform.position.x), Convert.ToSingle((Mathf.Sin(mouseAngle)*length)+transform.position.y)), Quaternion.identity,projectileHolder);
        obj.GetComponent<RangeProjectileScript>().Set(currentWeapon,gameObject);
    }

    void ProjectileAttack()
    {
        float length = 1.4f;
        if(projectilesUsed < currentWeapon.amount)
        {
            AddProjectile();
            GameObject proj = Instantiate(currentWeapon.projectile, new Vector2(Convert.ToSingle((Mathf.Cos(mouseAngle) * length) + transform.position.x), Convert.ToSingle((Mathf.Sin(mouseAngle) * length) + transform.position.y)), Quaternion.identity,projectileHolder);
            proj.name = currentWeapon.weaponName;
            proj.GetComponent<ProjectileScript>().Set(currentWeapon,gameObject);
        }
    }

    float tempMouseAngle;

    void ShowWeapon()
    {
        float ReturnSpeed = 0.4f;
        wpnObject.transform.position = Vector2.MoveTowards(wpnObject.transform.position, new Vector2(transform.position.x + (Mathf.Cos(tempMouseAngle) * cosmeticWeaponDistance), transform.position.y + (Mathf.Sin(tempMouseAngle) * cosmeticWeaponDistance)), ReturnSpeed);
        if (!WeaponObject.isSwinging)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - wpnObject.transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y*100, diff.x*100) * Mathf.Rad2Deg;

            tempMouseAngle = Mathf.Atan2(mousePosition.y- transform.position.y, mousePosition.x - transform.position.x);

            switch (currentWeapon.category)
            {
                case 0:
                    wpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                case 1:
                    wpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                case 2:
                    wpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                default:
                    break;
            }

            if ((Math.Abs(mouseAngle * Mathf.Rad2Deg)) >= 90)
            {
                wpnObject.transform.localScale = new Vector3(1, -1, 1);
                weaponObjectIsFlipped = true;
            }
            else 
            {
                wpnObject.transform.localScale = new Vector3(1, 1, 1);
                weaponObjectIsFlipped = false;
            }
        }
    }

    void PlayerEyeAnimation()
    {
        if (playerEyeCurrentAnimationCooldown > 0)
        {
            playerEyeCurrentAnimationCooldown--;
        }
        else
        {
            playerEyeAnimationCooldown = UnityEngine.Random.Range(0, 10000);
            playerEyeCurrentAnimationCooldown = playerEyeAnimationCooldown;
        }

        playerEyeAnimator.SetInteger("BlinkTimer", playerEyeCurrentAnimationCooldown);
    }

    public void AppendInventory(WeaponData.Weapon WantedWeapon)
    {
        inventoryWeapon.Add(new WeaponData.Weapon(WantedWeapon));
    }

    void Regen()
    {
        if(HPRegenClock == 0)
        {
            if (HP < HPMax)
            {
                if ((HP + HPRegen > HPMax) && (HP < HPMax)) HP = HPMax;
                else HP += HPRegen;
            }
        }
        HPRegenClock++;
        if (HPRegenClock >= HPRegenTime + 1) //>= so that HPRegenClock has 100 variants
            HPRegenClock = 0;
    }

    public static void MinusProjectile()
    {
        projectilesUsed--;
    }

    public static void AddProjectile()
    {
        projectilesUsed++;
    }
}
