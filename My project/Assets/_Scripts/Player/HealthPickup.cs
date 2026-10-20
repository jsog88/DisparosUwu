using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Configuración de Curación")]
    public int healAmount = 2; // Cantidad de vida que recupera este botiquín

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien toca el botiquín es el Jugador
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats == null)
            {
                stats = other.GetComponentInParent<PlayerStats>();
            }

            if (stats != null)
            {
                // Intenta curar al jugador
                bool wasHealed = stats.TryHeal(healAmount);

                // Solo si NO estaba al máximo y SÍ se pudo curar, destruye el objeto del suelo
                if (wasHealed)
                {
                    Debug.Log("¡Botiquín recogido y consumido!");
                    Destroy(gameObject);
                }
            }
        }
    }
}