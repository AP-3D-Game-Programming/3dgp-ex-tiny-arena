using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float range;

    // Penetratie-instellingen
    public int maxPenetrations = 999;     // aantal enemies dat geraakt mag worden
    public bool stopOnWall = true;        // stopt als je een muur raakt
    public float damageFalloff = 0f;      // schade verlies per penetratie

    public Color spellColor = Color.white; 

    public abstract void Cast(Camera cam);

    protected void RaycastThroughTargets(Camera cam, float range, float baseDamage)
    {
        Vector3 origin = cam.transform.position;
        Vector3 direction = cam.transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(origin, direction, range);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        int penetrations = 0;
        float currentDamage = baseDamage;

        foreach (RaycastHit hit in hits)
        {
            // Stop bij muren indien nodig
            if (stopOnWall && hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                break;

            Enemy e = hit.transform.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(currentDamage);
                Debug.Log($"Hit {hit.transform.name} voor {currentDamage} dmg");

                penetrations++;

                if (penetrations >= maxPenetrations)
                    break;

                // damage falloff bij volgende target
                currentDamage -= damageFalloff;
                currentDamage = Mathf.Max(0, currentDamage);
            }
        }
    }

    public virtual void PlayTrailFX(Transform staffTip, Camera cam, Color color)
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
