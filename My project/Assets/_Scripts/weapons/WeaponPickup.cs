using UnityEngine;
public class WeaponPickup : MonoBehaviour
{
    [Header("ScriptableObject del Arma")]
    public Guns weaponData; // Arrastra aquí el SO correspondiente (ej. Shotgun)

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que el jugador haya pisado el arma en el suelo
        if (other.CompareTag("Player"))
        {
            if (GunController.instance != null && weaponData != null)
            {
                // Le pasamos el nuevo ScriptableObject a tu GunController
                GunController.instance.EquipNewGun(weaponData);

                // Destruimos el pickup del suelo
                Destroy(gameObject);
            }
        }
    }
}