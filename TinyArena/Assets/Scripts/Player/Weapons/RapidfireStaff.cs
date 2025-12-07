using UnityEngine;

public class RapidfireStaff : Weapon
{
    public Animator bowAnim;

    //public override void OnStartFire()
    //{
    //    bowAnim.SetBool("Pulling", true);
    //}

    //public override void OnStopFire()
    //{
    //    bowAnim.SetBool("Pulling", false);
    //}

    public override void Shoot(Camera cam)
    {
        bowAnim.SetTrigger("Shoot");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 200f))
        {
            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null) e.TakeDamage(damage);
        }
    }
}
