using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName = "Weapon";
    public float fireRate = 0.5f;
    public float damage = 25f;

    public abstract void Shoot(Camera cam);
    //public abstract void OnStartFire();
    //public abstract void OnStopFire();
}
