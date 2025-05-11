using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponController : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public bool isReloading = false;
    public float reloadTime = 3f;

    public PlayerHUD hud;

    public GameObject bulletPrefab;       // Prefab de la bala
    public Transform firePoint;           // Lugar desde donde se disparan las balas
    public float bulletForce = 20f;        // Velocidad de la bala

    public Camera playerCamera;           // Cámara del jugador para apuntar

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

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

        if (bulletPrefab != null && firePoint != null && playerCamera != null)
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(1000); // Punto lejano si no colisiona
            }

            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.Initialize(shootDirection);
            }
        }
        else
        {
            Debug.LogWarning("bulletPrefab, firePoint o playerCamera no están asignados en el Inspector.");
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
