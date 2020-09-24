using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGeneral : MonoBehaviour
{
    public bool IsFollowing;
    public float AttentionRange = 1f, PickupRange = 0.2f ,PickupSpeed = 0.5f;
    public GameObject Player;
    public int WeaponID;
    public string WeaponName;
    public string[] WeaponNameArray = { "Copper shortsword", "Tin shortsword", "Iron shortsword", "Spear", "Amethyst staff", "Topaz staff", "Sapphire staff", "Amber staff", "Lead shortsword", "Silver shortsword", "Tungsten shortsword", "Iron broadsword", "Lead broadsword", "Silver broadsword", "Tungsten broadsword", "Emerald staff", "Ruby staff", "Crimson shortsword", "Gold shortsword", "Crimson broadsword", "Gold broadsword", "Trident", "Glaive", "Platinum shortsword", "Katana", "Platinum broadsword", "Diamond staff", "Last prism", "Star Wrath", "Phantasm", "Meowmere", "Celebration MK.2", "Solar Eruption", "Holy Water", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test", "test" };

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float DistToPlayer = Vector2.Distance(transform.position, Player.transform.position);
        if (IsFollowing)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,PickupSpeed);
        }
        if (DistToPlayer <= AttentionRange)
        {
            IsFollowing = true;
        }
        if (DistToPlayer <= PickupRange)
        {
            Player.GetComponent<PlayerGeneral>().AppendInventory(1,WeaponID,WeaponNameArray[WeaponID]);
            Destroy(gameObject);
        }
    }
}
