using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Configuración")]
    public Transform weaponHolder; // Punto donde se instanciarán las armas en la mano/cámara del jugador
    public int maxWeapons = 3;      // Cantidad máxima de armas a cargar

    // Estructura simple para almacenar el arma activa y su munición actual
    [System.Serializable]
    public class WeaponData
    {
        public GameObject weaponPrefab;
        public GameObject instantiatedWeapon; // Instancia física en la mano
        public int currentAmmo;
    }

    [Header("Estado del Inventario")]
    public List<WeaponData> inventory = new List<WeaponData>();
    private int currentWeaponIndex = -1;

    void Update()
    {
        HandleWeaponSwitching();

        // Ejemplo de disparo al hacer Clic Izquierdo (Adapta según tu lógica de disparo)
        if (Input.GetButtonDown("Fire1"))
        {
            UseAmmo(1); // Resta 1 bala por disparo
        }
    }

    public bool AddWeapon(GameObject newWeaponPrefab, int ammo)
    {
        // Si ya tenemos el número máximo de armas, no recoge más
        if (inventory.Count >= maxWeapons)
        {
            Debug.Log("Inventario lleno.");
            return false;
        }

        // Crear la instancia del arma en la mano pero mantenerla desactivada de inicio
        GameObject spawnedWeapon = Instantiate(newWeaponPrefab, weaponHolder);
        spawnedWeapon.transform.localPosition = Vector3.zero;
        spawnedWeapon.transform.localRotation = Quaternion.identity;
        spawnedWeapon.SetActive(false);

        WeaponData newWeapon = new WeaponData
        {
            weaponPrefab = newWeaponPrefab,
            instantiatedWeapon = spawnedWeapon,
            currentAmmo = ammo
        };

        inventory.Add(newWeapon);

        // Si es la única arma que tenemos, equiparla inmediatamente
        if (inventory.Count == 1)
        {
            EquipWeapon(0);
        }

        return true;
    }

    void HandleWeaponSwitching()
    {
        // Detecta las teclas del 1 al 9
        for (int i = 0; i < inventory.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipWeapon(i);
            }
        }
    }

    void EquipWeapon(int index)
    {
        if (index < 0 || index >= inventory.Count) return;

        // Desactivar el arma actual
        if (currentWeaponIndex >= 0 && currentWeaponIndex < inventory.Count)
        {
            inventory[currentWeaponIndex].instantiatedWeapon.SetActive(false);
        }

        // Activar la nueva arma
        currentWeaponIndex = index;
        inventory[currentWeaponIndex].instantiatedWeapon.SetActive(true);
    }

    // Llama a esta función cada vez que el jugador dispara
    public void UseAmmo(int amount)
    {
        if (currentWeaponIndex < 0 || inventory.Count == 0) return;

        WeaponData activeWeapon = inventory[currentWeaponIndex];
        activeWeapon.currentAmmo -= amount;

        Debug.Log($"Munición restante: {activeWeapon.currentAmmo}");

        // Si se terminan las balas, eliminar el arma del inventario
        if (activeWeapon.currentAmmo <= 0)
        {
            RemoveCurrentWeapon();
        }
    }

    void RemoveCurrentWeapon()
    {
        if (currentWeaponIndex < 0 || inventory.Count == 0) return;

        // Destruye el GameObject en la mano
        Destroy(inventory[currentWeaponIndex].instantiatedWeapon);

        // Elimina el registro del inventario
        inventory.RemoveAt(currentWeaponIndex);

        // Selecciona otra arma si aún quedan disponibles
        if (inventory.Count > 0)
        {
            // Equipa la previa o la primera de la lista
            int nextIndex = Mathf.Clamp(currentWeaponIndex - 1, 0, inventory.Count - 1);
            EquipWeapon(nextIndex);
        }
        else
        {
            currentWeaponIndex = -1; // Se quedó sin armas
            Debug.Log("Sin armas en el inventario.");
        }
    }
}
