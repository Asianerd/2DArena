using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootGeneral : MonoBehaviour
{
    public bool IsFollowing;
    public float AttentionRange = 1f, PickupRange = 0.2f ,PickupSpeed = 0.5f;
    public GameObject Player;
    public PlayerGeneral.Weapon CurrentWeapon;

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
        Debug.Log(CurrentWeapon.WeaponName);
        GetComponentInChildren<Text>().text = CurrentWeapon.WeaponName;
        //GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = CurrentWeapon.WeaponName;
    }

    void Update()
    {
        float DistToPlayer = Vector2.Distance(transform.position, Player.transform.position);
        if (IsFollowing) transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,PickupSpeed);
        if (DistToPlayer <= AttentionRange) IsFollowing = true;
        if (DistToPlayer <= PickupRange)
        {
            Player.GetComponent<PlayerGeneral>().AppendInventory(1, CurrentWeapon);
            Destroy(gameObject);
        }
        
    }
}
