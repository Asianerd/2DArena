using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericRangeProjectile : MonoBehaviour
{
    public double DirectionAngle;
    public float Speed;
    public int ShelfLife;
    public double IncrementX, IncrementY;

    public void Set(double angle,float speed,int shelflife)
    {
        DirectionAngle = angle;
        Speed = speed;
        ShelfLife = shelflife;
        IncrementX = Math.Cos(DirectionAngle) * Speed;
        IncrementY = Math.Sin(DirectionAngle) * Speed;
    }

    void Update()
    {
        if (ShelfLife > 0)
        {
            ShelfLife--;
            transform.position = new Vector2(transform.position.x+Convert.ToSingle(IncrementX), transform.position.y+Convert.ToSingle(IncrementY));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
