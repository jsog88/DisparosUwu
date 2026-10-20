using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien toca el objeto es el Jugador
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats == null)
            {
                stats = other.GetComponentInParent<PlayerStats>();
            }

            if (stats != null)
            {
                // Activa la armadura y llena la barra
                stats.EnableArmor();

                Debug.Log("¡Pickup de Armadura recogido!");
                Destroy(gameObject); // Destruye el ítem del suelo
            }
            else
            {
                Debug.LogError("No se encontró el script PlayerStats en el Jugador.");
            }
        }
    }
}