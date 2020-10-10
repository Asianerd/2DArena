using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectileSpawner : MonoBehaviour
{
    public int AmountUsed;
    public WeaponData.Weapon ProjectileWeapon;
    public List<GameObject> ProjectileSpawned;

    public void Throw(WeaponData.Weapon CurrentWeapon)
    {
        ProjectileWeapon = CurrentWeapon;
        if(AmountUsed < ProjectileWeapon.Amount)
        {
            GameObject obj = Instantiate(ProjectileWeapon.Projectile,PlayerGeneral.PlayerPosition,Quaternion.identity);
            obj.GetComponent<GenericProjectile>().Set(ProjectileWeapon,gameObject);
            AmountUsed--;
        }
    }

    public void ProjectileDestroy()
    {

    }

    public void Empty()
    {
        AmountUsed = 0;
        foreach(GameObject i in ProjectileSpawned) Destroy(i);
        ProjectileSpawned = new List<GameObject>();
    }
}
