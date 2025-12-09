using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public Transform staffTransform;  //  voeg deze toe in de Inspector

    public SpellManager spellManager;

    private bool isHolding = false;
    private float nextShot = 0f;

    private Quaternion originalRotation = Quaternion.Euler(-90, 0, 0);
    private Quaternion firingRotation = Quaternion.Euler(-35, 0, 0);

    private Coroutine rotateRoutine;   //  zorgt dat animaties niet overlappen


    public void StartFiring()
    {
        isHolding = true;

        // start rotatie naar firing pose
        StartRotate(firingRotation, 0.1f);

        StartCoroutine(FireLoop());
    }

    public void StopFiring()
    {
        isHolding = false;

        // rotatie terug naar originele
        StartRotate(originalRotation, 0.15f);
    }

    IEnumerator FireLoop()
    {
        while (isHolding)
        {
            TryShoot();
            yield return null;
        }
    }

    void TryShoot()
    {
        Spell spell = spellManager.CurrentSpell;
        if (spell == null) return;
        if (Time.time < nextShot) return;

        nextShot = Time.time + spell.fireRate;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        spell.Cast(playerCamera);
        // spawn visueel effect aan staff
        spell.PlayTrailFX(staffTransform, playerCamera, spell.spellColor);

        //// Raycast from center of screen
        //RaycastHit hit;
        //if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        //{
        //    Debug.Log($"Hit: {hit.transform.name}");

        //    // Check if we hit an enemy
        //    Enemy enemy = hit.transform.GetComponent<Enemy>();
        //    if (enemy != null)
        //    {
        //        enemy.TakeDamage(damage);
        //    }

        //    // Spawn impact effect
        //    if (impactEffect != null)
        //    {
        //        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //        Destroy(impact, 2f);
        //    }
        //}
    }


    // -------------------------------
    // ROTATIE-SYSTEEM
    // -------------------------------

    private void StartRotate(Quaternion targetRot, float duration)
    {
        if (staffTransform == null) return;

        // stop lopende animatie zodat ze elkaar niet overlappen
        if (rotateRoutine != null)
            StopCoroutine(rotateRoutine);

        rotateRoutine = StartCoroutine(SmoothRotate(staffTransform, targetRot, duration));
    }

    private IEnumerator SmoothRotate(Transform target, Quaternion endRot, float duration)
    {
        Quaternion startRot = target.localRotation;
        float t = 0f;

        while (t < duration)
        {
            target.localRotation = Quaternion.Lerp(startRot, endRot, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        target.localRotation = endRot;
    }


}
