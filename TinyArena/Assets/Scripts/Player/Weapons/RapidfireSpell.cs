using UnityEngine;

public class RapidfireSpell : Spell
{
    private void Awake()
    {
        fireRate = 0.1f;
        damage = 10f;
        range = 150f;

        maxPenetrations = 5;
        stopOnWall = false;   // gaat door muren
        damageFalloff = 2f;   // elke enemy 2 dmg minder

        spellColor = Color.cyan;

        maxPenetrations = 2;
        stopOnWall = true;
        damageFalloff = 2;
    }

    public override void OnHit(RaycastHit hit)
    {
        // Rapidfire doet zelf een penetratie raycast vanuit het impactpunt
        Collider[] hits = Physics.OverlapSphere(hit.point, 0.25f);
        foreach (var h in hits)
        {
            Enemy e = h.GetComponent<Enemy>();
            if (e != null)
                e.TakeDamage(damage);
        }
    }

    //public override void Cast(Camera cam)
    //{
    //    RaycastThroughTargets(cam, range, damage);
    //}

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

