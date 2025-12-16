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

        maxPenetrations = 999;
        stopOnWall = true;
        damageFalloff = 0;
    }

    //public override void Cast(Camera cam)
    //{
    //    // 1. eerst de directe raycast: waar explodeert de fireball?
    //    Vector3 origin = cam.transform.position;
    //    Vector3 direction = cam.transform.forward;

    //    if (Physics.Raycast(origin, direction, out RaycastHit hit, range))
    //    {
    //        // Hier explodeert de Fireball
    //        Explode(hit.point);
    //    }
    //}

    public override void OnHit(RaycastHit hit)
    {
        Explode(hit.point);
    }

    // -------------------------
    //   FIREBALL EXPLOSIE
    // -------------------------
    private void Explode(Vector3 explosionPoint)
    {
        int rays = 64;                     // aantal richtingen
        float explosionRange = 10f;         // explosie radius
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

    public override void PlayTrailFX(Transform staffTip, Camera cam, Color color)
    {
        if (SpellManager.Instance == null || SpellManager.Instance.spellTrailFX == null)
            return;

        // Spawn trail MIDDEN in de kijkrichting, niet staf-rotatie
        ParticleSystem fx = Instantiate(
            SpellManager.Instance.spellTrailFX,
            staffTip.position,
            cam.transform.rotation
        );

        var main = fx.main;
        main.startColor = color;

        var trail = fx.GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.startColor = color;
            trail.endColor = color;
        }

        Destroy(fx.gameObject, 2f);
    }
}
