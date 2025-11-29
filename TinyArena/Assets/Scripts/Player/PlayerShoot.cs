using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.5f;
    public int maxAmmo = 30;
    public float reloadTime = 2f;

    [Header("References")]
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    public void Shoot()
    {
        if (isReloading || currentAmmo <= 0 || Time.time < nextTimeToFire)
            return;

        nextTimeToFire = Time.time + fireRate;
        currentAmmo--;
        Debug.Log($"Shot! Ammo: {currentAmmo}/{maxAmmo}");

        // Play muzzle flash
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Raycast from center of screen
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log($"Hit: {hit.transform.name}");

            // Check if we hit an enemy
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Spawn impact effect
            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }

        // Auto reload when out of ammo
        if (currentAmmo <= 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        if (!isReloading && currentAmmo < maxAmmo)
        {
            isReloading = true;
            Debug.Log("Reloading...");
            Invoke(nameof(FinishReload), reloadTime);
        }
    }

    void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload complete!");
    }
}