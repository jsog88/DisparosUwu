using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;

    [Header("Configuración de Arma")]
    public Guns gun; // ScriptableObject actual
    public AudioSource audioSource;
    public LayerMask raycastLayerMask; // Capa para detectar enemigos

    [Header("Munición")]
    public int currentAmmo;

    private bool canFire;
    private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            mainCamera = GetComponentInChildren<Camera>();
        }

        canFire = true;

        if (gun != null)
        {
            EquipNewGun(gun);
        }
    }

    public void EquipNewGun(Guns newGun)
    {
        if (newGun == null) return;

        gun = newGun;
        currentAmmo = gun.maxAmmo;

        // Actualizar el modelo visual 3D en las manos del jugador
        PlayerWeaponHandler weaponHandler = GetComponent<PlayerWeaponHandler>();
        if (weaponHandler == null)
        {
            weaponHandler = GetComponentInParent<PlayerWeaponHandler>();
        }

        if (weaponHandler != null)
        {
            weaponHandler.UpdateWeaponVisual(newGun.weaponPrefab);
        }

        Debug.Log($"Arma equipada en GunController: {gun.name}");
    }

    IEnumerator CanFire()
    {
        canFire = false;
        yield return new WaitForSeconds(gun.fireRate);
        canFire = true;
    }

    public void Fire()
    {
        if (gun == null || currentAmmo <= 0) return;

        if (canFire)
        {
            if (gun.sound != null && audioSource != null)
            {
                audioSource.PlayOneShot(gun.sound);
            }

            currentAmmo--;

            // Si la máscara de capa no está asignada en el Inspector, usa la capa Default para no fallar
            LayerMask maskToUse = (raycastLayerMask.value == 0) ? Physics.DefaultRaycastLayers : raycastLayerMask;

            // Disparo mediante Raycast proyectado desde el centro exacto de la pantalla
            if (mainCamera != null)
            {
                Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;

                // Lanza un rayo frontal a la distancia indicada por el ScriptableObject (gun.range)
                if (Physics.Raycast(ray, out hit, gun.range, maskToUse))
                {
                    // Busca el componente Enemy en el objeto impactado o en sus padres
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy == null)
                    {
                        enemy = hit.transform.GetComponentInParent<Enemy>();
                    }

                    if (enemy != null)
                    {
                        Quaternion rot = Quaternion.LookRotation(-hit.normal);
                        enemy.Damage(gun.damage, rot);
                        Debug.Log($"¡IMPACTO CONFIRMADO! Enemigo {enemy.name} recibió {gun.damage} de daño.");
                    }
                    else
                    {
                        Debug.Log($"Raycast golpeó {hit.transform.name}, pero no tiene script Enemy.");
                    }
                }
                else
                {
                    Debug.Log("El disparo no impactó ningún objeto dentro del rango.");
                }
            }

            StartCoroutine(CanFire());

            if (currentAmmo <= 0)
            {
                RemoveGun();
            }
        }
    }

    public void RemoveGun()
    {
        gun = null;
        currentAmmo = 0;

        PlayerWeaponHandler weaponHandler = GetComponent<PlayerWeaponHandler>();
        if (weaponHandler == null)
        {
            weaponHandler = GetComponentInParent<PlayerWeaponHandler>();
        }

        if (weaponHandler != null)
        {
            weaponHandler.UpdateWeaponVisual(null);
        }

        Debug.Log("Sin balas. Arma desequipada.");
    }

    public bool TryAddAmmo(int amount)
    {
        if (gun == null)
        {
            Debug.Log("No tienes ningún arma equipada para recargar.");
            return false;
        }

        if (currentAmmo >= gun.maxAmmo)
        {
            Debug.Log("El arma ya tiene la munición al máximo.");
            return false;
        }

        currentAmmo = Mathf.Min(currentAmmo + amount, gun.maxAmmo);
        Debug.Log($"Munición recargada. Balas actuales: {currentAmmo} / {gun.maxAmmo}");

        return true;
    }
}