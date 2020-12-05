using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootGeneral : MonoBehaviour
{
    public bool isFollowing;
    public float attentionRange = 3f, pickupRange = 0.2f ,pickupSpeed = 0.5f;
    public GameObject player;
    public WeaponData.Weapon currentWeapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }

    public void SetWeapon(WeaponData.Weapon SelectedWeapon,Sprite SelectedSprite)
    {
        currentWeapon = SelectedWeapon;
        GetComponentInChildren<Text>().text = currentWeapon.weaponName;
        GetComponentInChildren<SpriteRenderer>().sprite = SelectedSprite;
    }

    void Update()
    {
        float DistToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (isFollowing) transform.position = Vector2.MoveTowards(transform.position, player.transform.position,pickupSpeed);
        if (DistToPlayer <= attentionRange) isFollowing = true;
        if (DistToPlayer <= pickupRange)
        {
            player.GetComponent<PlayerGeneral>().AppendInventory(currentWeapon);
            Destroy(gameObject);
        }
    }
}
