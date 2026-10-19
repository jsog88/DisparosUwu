using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Referencia al punto de origen en la Cámara")]
    public Transform weaponHolder; // Arrastra el objeto 'WeaponHolder' (hijo de Main Camera)

    private GameObject currentWeaponInstance;

    /// <summary>
    /// Reemplaza o destruye el modelo 3D visual en las manos del jugador
    /// </summary>
    public void UpdateWeaponVisual(GameObject newWeaponPrefab)
    {
        // 1. Destruye el modelo 3D actual si existía uno
        if (currentWeaponInstance != null)
        {
            Destroy(currentWeaponInstance);
        }

        // 2. Si no hay nuevo prefab (se acabaron las balas), termina aquí
        if (newWeaponPrefab == null || weaponHolder == null) return;

        // 3. Instancia el nuevo modelo 3D en la mano
        currentWeaponInstance = Instantiate(newWeaponPrefab, weaponHolder);

        // Posicionamiento relativo frente a la cámara
        currentWeaponInstance.transform.localPosition = new Vector3(0.3f, -0.3f, 0.5f);
        currentWeaponInstance.transform.localRotation = Quaternion.identity;
        currentWeaponInstance.transform.localScale = Vector3.one;
    }
}