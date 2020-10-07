using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootGeneral : MonoBehaviour
{
    public bool IsFollowing;
    public float AttentionRange = 1f, PickupRange = 0.2f ,PickupSpeed = 0.5f;
    public GameObject Player;
    public WeaponData.Weapon CurrentWeapon;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon,Sprite SelectedSprite)
    {
        CurrentWeapon = SelectedWeapon;
        GetComponentInChildren<Text>().text = CurrentWeapon.WeaponName;
        GetComponentInChildren<SpriteRenderer>().sprite = SelectedSprite;
    }

    void Update()
    {
        float DistToPlayer = Vector2.Distance(transform.position, Player.transform.position);
        if (IsFollowing) transform.position = Vector2.MoveTowards(transform.position, Player.transform.position,PickupSpeed);
        if (DistToPlayer <= AttentionRange) IsFollowing = true;
        if (DistToPlayer <= PickupRange)
        {
            Player.GetComponent<PlayerGeneral>().AppendInventory(CurrentWeapon);
            Destroy(gameObject);
        }
    }
}
