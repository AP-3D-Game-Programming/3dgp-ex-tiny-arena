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
    }

    public override void Cast(Camera cam)
    {
        RaycastThroughTargets(cam, range, damage);
    }
}

