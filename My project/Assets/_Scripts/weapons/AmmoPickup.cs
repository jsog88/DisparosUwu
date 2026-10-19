using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Configuración de Munición")]
    public int ammoAmount = 15; // Cantidad de balas que recarga esta caja

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si es el Jugador
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            if (GunController.instance != null)
            {
                // Intenta añadir la munición al GunController
                bool wasPickedUp = GunController.instance.TryAddAmmo(ammoAmount);

                // Solo si el arma NO estaba llena y SÍ se pudo recargar, se destruye la caja
                if (wasPickedUp)
                {
                    Debug.Log("¡Caja de munición recogida!");
                    Destroy(gameObject);
                }
            }
        }
    }
}