using UnityEngine;
using System.Collections;

public class FireballStaff : Weapon
{
    public ParticleSystem flash;

    public float damage = 150f;
    public float range = 100f;
    public float fireRate = 2f;

    //public override void OnStartFire()
    //{
    //    if (flash != null)
    //        flash.Play();
    //}

    //public override void OnStopFire()
    //{
    //    if (flash != null)
    //        flash.Stop();
    //}

    public override void Shoot(Camera cam)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
        {
            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null) e.TakeDamage(damage);
        }
    }
}
