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
    public Canvas InvCanvas;
    public GameObject RuntimeScript;
    public GameObject DamageBubblePrefab;
    public Transform ProjectileHolder;

    public Camera PlayerCamera;
    public static Vector3 MousePosition;
    public static float MouseAngle;
    public static Vector2 PlayerPosition;

    public GameObject WpnObject;
    public float CosmeticWeaponDistance = 1f;

    //HP
    public float HP = 100, HPMax = 100, HPRegen = 1;
    public int HPRegenTime = 100, HPRegenClock = 0;
    public static List<WeaponData.Weapon> InventoryWeapon = new List<WeaponData.Weapon>();
    public List<ArrayList> InventoryLoot = new List<ArrayList>();
    public List<string> InventoryLootName = new List<string>();
    public WeaponData.Weapon EquippedWeapon;

    //ATK
    public GameObject[] EnemyArray;
    public float PlayerDamage = 3f;

    public WeaponData.Weapon CurrentWeapon;
    public static WeaponData.Weapon CurrentWeaponReference;

    public int WeaponCooldown = 0;

    //Projectile data
    public List<GameObject> ProjectileSpawned;
    public static int ProjectilesUsed;

    //Animation data
    public Animator PlayerEyeAnimator;
    public int PlayerEyeAnimationCooldown;
    int PlayerEyeCurrentAnimationCooldown = 0;



    public static bool WeaponObjectIsFlipped = false;

    public GameObject TempText;

    public void ResetCurrentWeapon()
    {
        CurrentWeapon = new WeaponData.Weapon("Fists", 50, 100, 1, 100, 0, 1, 0, 0, 0);
    }

    void Awake()
    {
        ResetCurrentWeapon();
        PlayerCamera = FindObjectOfType<Camera>();
        PlayerEyeAnimationCooldown = UnityEngine.Random.Range(0, 10000);
    }

    void Update()
    {
        MousePosition = PlayerCamera.ScreenToWorldPoint(Input.mousePosition);

        MouseAngle = Mathf.Atan2(MousePosition.y - transform.position.y, MousePosition.x - transform.position.x);

        PlayerPosition = new Vector2(transform.position.x, transform.position.y);

        CurrentWeaponReference = CurrentWeapon;

        TempText.GetComponent<Text>().text = $"LVL : {CurrentWeapon.Level}  {CurrentWeapon.LevelCurrentProgression}/{CurrentWeapon.LevelNextLevelProgression}";

        // Remove please - taking 70ish fps to execute
        // Sets ProjectilesUsed to the amount of GameObjects with the same name
        // Just increase/decrease when a projectile is instantiated/destroyed
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

        CurrentWeapon.LevelCheck();

        // Debug keys
        if (Input.GetKey(KeyCode.C))
        {
            int SpawnedWeaponID = UnityEngine.Random.Range(0, WeaponData.GlobalWeaponList.Count);
            RuntimeScript.GetComponent<LootSpawning>().SpawnWeaponLoot(transform.position, WeaponData.GlobalWeaponList[SpawnedWeaponID]);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.position = new Vector2(MousePosition.x, MousePosition.y);
        }
        // Debug keys

        if (!InventoryGeneral.GamePaused)
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
            CurrentWeapon.Durability -= LostDurability;
        else
            if (CurrentWeapon.IsBreakable && CurrentWeapon.Durability > 0)
                CurrentWeapon.Durability -= LostDurability;
    }

    public void AddWeaponLevelProgress(int WeaponProgress)
    {
        CurrentWeapon.LevelCurrentProgression += WeaponProgress/10;
        CurrentWeapon.LevelCheck();
    }

    void WeaponDurabilityCheck()
    {
        if ((CurrentWeapon.Durability <= 0) && !(CurrentWeapon.Durability == -100))
        {
            ResetCurrentWeapon();
        }
    }

    void Attack()
    {
        if ((WeaponCooldown == 0) && Input.GetMouseButton(0))
        {
            WeaponCooldown = CurrentWeapon.WeaponCooldown;
            switch (CurrentWeapon.Category)
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
        if (WeaponCooldown > 0) WeaponCooldown--;
    }

    void MeleeAttack()
    {        
        WpnObject.GetComponent<WeaponObject>().SwingWrapper();
    }

    void RangeAttack()
    {
        float length = 1.4f;
        GameObject obj = Instantiate(CurrentWeapon.RangeProjectile, new Vector2(Convert.ToSingle((Mathf.Cos(MouseAngle)*length)+transform.position.x), Convert.ToSingle((Mathf.Sin(MouseAngle)*length)+transform.position.y)), Quaternion.identity,ProjectileHolder);
        obj.GetComponent<RangeProjectileScript>().Set(CurrentWeapon,gameObject);
    }

    void ProjectileAttack()
    {
        float length = 1.4f;
        if(ProjectilesUsed < CurrentWeapon.Amount)
        {
            AddProjectile();
            GameObject proj = Instantiate(CurrentWeapon.Projectile, new Vector2(Convert.ToSingle((Mathf.Cos(MouseAngle) * length) + transform.position.x), Convert.ToSingle((Mathf.Sin(MouseAngle) * length) + transform.position.y)), Quaternion.identity,ProjectileHolder);
            proj.name = CurrentWeapon.WeaponName;
            proj.GetComponent<ProjectileScript>().Set(CurrentWeapon,gameObject);
        }
    }

    float TempMouseAngle;

    void ShowWeapon()
    {
        float ReturnSpeed = 0.4f;
        WpnObject.transform.position = Vector2.MoveTowards(WpnObject.transform.position, new Vector2(transform.position.x + (Mathf.Cos(TempMouseAngle) * CosmeticWeaponDistance), transform.position.y + (Mathf.Sin(TempMouseAngle) * CosmeticWeaponDistance)), ReturnSpeed);
        if (!WeaponObject.IsSwinging)
        {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - WpnObject.transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y*100, diff.x*100) * Mathf.Rad2Deg;

            TempMouseAngle = Mathf.Atan2(MousePosition.y- transform.position.y, MousePosition.x - transform.position.x);

            switch (CurrentWeapon.Category)
            {
                case 0:
                    WpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                case 1:
                    WpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                case 2:
                    WpnObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
                    break;
                default:
                    break;
            }

            if ((Math.Abs(MouseAngle * Mathf.Rad2Deg)) >= 90)
            {
                WpnObject.transform.localScale = new Vector3(1, -1, 1);
                WeaponObjectIsFlipped = true;
            }
            else 
            {
                WpnObject.transform.localScale = new Vector3(1, 1, 1);
                WeaponObjectIsFlipped = false;
            }
        }
    }

    void PlayerEyeAnimation()
    {
        if (PlayerEyeCurrentAnimationCooldown > 0)
        {
            PlayerEyeCurrentAnimationCooldown--;
        }
        else
        {
            PlayerEyeAnimationCooldown = UnityEngine.Random.Range(0, 10000);
            PlayerEyeCurrentAnimationCooldown = PlayerEyeAnimationCooldown;
        }

        PlayerEyeAnimator.SetInteger("BlinkTimer", PlayerEyeCurrentAnimationCooldown);
    }

    public void AppendInventory(WeaponData.Weapon WantedWeapon)
    {
        InventoryWeapon.Add(new WeaponData.Weapon(WantedWeapon));
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
        ProjectilesUsed--;
    }

    public static void AddProjectile()
    {
        ProjectilesUsed++;
    }
}
