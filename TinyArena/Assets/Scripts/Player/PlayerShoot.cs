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

    [Header("Audio")]
    public AudioClip shootSound;

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

        // 1. Raycast uitvoeren (centraal in player)
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, spell.range))
        {
            // 2. Spell krijgt de hit en beslist wat er gebeurt
            spell.OnHit(hit);
        }

        // 3. Trail effect laten zien
        spell.PlayTrailFX(staffTransform, playerCamera, spell.spellColor);

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySpell(shootSound);
        }
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
