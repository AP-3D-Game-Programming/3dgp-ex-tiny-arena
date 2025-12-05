using UnityEngine;
using System.Collections;

public class DefaultStaff : Weapon
{
    public Transform wand;
    public float turnAmount = 55f;
    public float turnSpeed = 0.1f;
    public float resetSpeed = 0.2f;

    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.5f;

    //public override void OnStartFire()
    //{
    //    if (wand == null) return;

    //    Quaternion rot = Quaternion.Euler(
    //        originalRotation.eulerAngles.x + turnAmount,
    //        originalRotation.eulerAngles.y,
    //        originalRotation.eulerAngles.z
    //    );

    //    StartCoroutine(SmoothRotate(wand, rot, turnSpeed));
    //}

    //public override void OnStopFire()
    //{
    //    if (wand == null) return;
    //    StartCoroutine(SmoothRotate(wand, originalRotation, resetSpeed));
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
