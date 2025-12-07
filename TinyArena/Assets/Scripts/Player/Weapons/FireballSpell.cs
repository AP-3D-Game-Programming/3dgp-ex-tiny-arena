using UnityEngine;

public class FireballSpell : Spell
{
    private void Awake()
    {
        fireRate = 1.2f;
        damage = 100f;
        range = 200f;

        // Fireball gebruikt *geen* penetratie logic
        maxPenetrations = 0;
        stopOnWall = true;
        damageFalloff = 0;

        spellColor = new Color(1f, 0.35f, 0f);  // oranje vuur
    }

    public override void Cast(Camera cam)
    {
        // 1. eerst de directe raycast: waar explodeert de fireball?
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
        {
            // Hier explodeert de Fireball
            Explode(hit.point);
        }
    }


    // -------------------------
    //   FIREBALL EXPLOSIE
    // -------------------------
    private void Explode(Vector3 explosionPoint)
    {
        int rays = 32;                     // aantal richtingen
        float explosionRange = 6f;         // explosie radius
        float explosionDamage = this.damage;

        for (int i = 0; i < rays; i++)
        {
            // willekeurige richting in een sphere
            Vector3 dir = Random.onUnitSphere;

            if (Physics.Raycast(explosionPoint, dir, out RaycastHit hit, explosionRange))
            {
                Enemy e = hit.transform.GetComponent<Enemy>();
                if (e != null)
                {
                    e.TakeDamage(explosionDamage);
                }
            }
        }

        Debug.Log("Fireball explosie op: " + explosionPoint);
    }
}
