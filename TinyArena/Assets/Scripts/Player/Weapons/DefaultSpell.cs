using System.Diagnostics;
using UnityEngine;

public class DefaultSpell : Spell
{
    private void Awake()
    {
        fireRate = 0.5f;
        damage = 25f;
        range = 100f;

        maxPenetrations = 999;
        stopOnWall = true;
        damageFalloff = 0f;

        spellColor = Color.purple;

        maxPenetrations = 999;
        stopOnWall = false;
        damageFalloff = 0;

    }

    public override void OnHit(RaycastHit hit)
    {
        Enemy e = hit.transform.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
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

    //public override void Cast(Camera cam)
    //{
    //    RaycastThroughTargets(cam, range, damage);
    //}
}
