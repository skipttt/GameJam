using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public bool isReloading = false;
    public float reloadTime = 3f;

    public PlayerHUD hud;

    public GameObject bulletPrefab;       // Prefab de la bala
    public Transform firePoint;           // Lugar desde donde se disparan las balas
    public float bulletForce = 20f;        // Fuerza de la bala

    void Start()
    {
        currentAmmo = maxAmmo;
        hud.SetAmmo(currentAmmo, int.MaxValue);
        hud.ShowOutOfAmmo(false);
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentAmmo > 0)
            {
                Shoot();
                hud.ShowOutOfAmmo(false);
            }
            else
            {
                Debug.Log("¡Sin balas! Presiona R para recargar.");
                hud.ShowOutOfAmmo(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    void Shoot()
    {
        
        if (currentAmmo <= 0)
        {
            Debug.Log("Bloqueo de disparo, sin balas.");
            return;
        }

        currentAmmo--;
        hud.SetAmmo(currentAmmo, int.MaxValue);
        Debug.Log("Disparo. Balas restantes: " + currentAmmo);

        // Instanciar la bala si todo está bien configurado
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 shootDirection = Camera.main.transform.forward;
                rb.AddForce(shootDirection * bulletForce, ForceMode.Impulse);
            }

            }
            else
            {
                Debug.LogWarning("bulletPrefab o firePoint no están asignados en el Inspector.");
            }

        if (currentAmmo == 0)
        {
            hud.ShowOutOfAmmo(true);
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        hud.ShowOutOfAmmo(false);
        Debug.Log("Recargando...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        hud.SetAmmo(currentAmmo, int.MaxValue);
        isReloading = false;
        Debug.Log("Recarga completada.");
    }
}
