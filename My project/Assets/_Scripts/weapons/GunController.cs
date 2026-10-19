using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;
    private BoxCollider gunTrigger;
    public Guns gun; // ScriptableObject actual
    public LayerMask raycastLayerMask;
    public AudioSource audioSource;

    [Header("Munición")]
    public int currentAmmo;

    private bool canFire;

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
        gunTrigger = GetComponent<BoxCollider>();
        canFire = true;

        if (gun != null)
        {
            EquipNewGun(gun);
        }
    }

    public void EquipNewGun(Guns newGun)
    {
        if (newGun == null) return;

        // 1. Asignar los datos del ScriptableObject
        gun = newGun;
        currentAmmo = gun.maxAmmo;
        SetTriggers();

        // 2. CONEXIÓN CON EL WEAPON HANDLER:
        // Busca el handler en el Jugador para actualizar el modelo 3D visual
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

    public void SetTriggers()
    {
        if (gunTrigger != null && gun != null)
        {
            gunTrigger.size = new Vector3(gun.horizontalRange, gun.verticalRange, gun.range);
            gunTrigger.center = new Vector3(0, (0.5f * gun.verticalRange - 1f), gun.range * 0.5f);
        }
    }

    IEnumerator CanFire()
    {
        canFire = false;
        yield return new WaitForSeconds(gun.fireRate);
        canFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            EnemyManager.instance.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            EnemyManager.instance.RemoveEnemy(enemy);
        }
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

            foreach (Enemy enemy in EnemyManager.instance.enemiesInRange)
            {
                var dir = (enemy.transform.position - transform.position).normalized;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, gun.range * 1.5f, raycastLayerMask))
                {
                    if (hit.transform == enemy.transform)
                    {
                        Quaternion rot = Quaternion.LookRotation(-hit.normal);
                        enemy.Damage(gun.damage, rot);
                    }
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

        if (gunTrigger != null)
        {
            gunTrigger.size = Vector3.zero;
        }

        // Remover también el modelo visual
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
        // Si no hay arma equipada o el SO no existe, no se puede recargar
        if (gun == null)
        {
            Debug.Log("No tienes ningún arma equipada para recargar.");
            return false;
        }

        // Si ya tiene la munición completa, no la recoge
        if (currentAmmo >= gun.maxAmmo)
        {
            Debug.Log("El arma ya tiene la munición al máximo.");
            return false;
        }

        // Le suma la munición sin sobrepasar el límite máximo del ScriptableObject (maxAmmo)
        currentAmmo = Mathf.Min(currentAmmo + amount, gun.maxAmmo);
        Debug.Log($"Munición recargada. Balas actuales: {currentAmmo} / {gun.maxAmmo}");

        return true; // Se recargó con éxito
    }
}