using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootGeneral : MonoBehaviour
{
    public bool IsFollowing;
    public float AttentionRange = 1f, PickupRange = 0.2f ,PickupSpeed = 0.5f;
    public GameObject Player;
    public int WeaponID;

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
            Debug.Log(WeaponID);
            Player.GetComponent<PlayerGeneral>().AppendInventory(1,WeaponID);
            Destroy(gameObject);
        }
    }
}
