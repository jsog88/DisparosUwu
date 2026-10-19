using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien tocó el pickup tiene el Tag 'Player'
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            // Busca el JetpackModule directamente en el objeto tocado o en la raíz del Jugador
            JetpackModule jetpack = other.GetComponent<JetpackModule>();
            if (jetpack == null)
            {
                jetpack = other.GetComponentInParent<JetpackModule>();
            }

            if (jetpack != null)
            {
                jetpack.EnableJetpack();
                Debug.Log("¡JETPACK ENTREGADO CON ÉXITO A: " + jetpack.gameObject.name + "!");
                Destroy(gameObject); // Elimina el pickup del suelo
            }
            else
            {
                Debug.LogError("El objeto '" + other.gameObject.name + "' colisionó pero NO tiene el script JetpackModule.");
            }
        }
    }
}