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
    }

    public override void Cast(Camera cam)
    {
        RaycastThroughTargets(cam, range, damage);
    }
}
