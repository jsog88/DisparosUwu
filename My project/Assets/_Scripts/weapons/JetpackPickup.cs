using UnityEngine;

public class JetpackPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Revisa si es el jugador
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            // 1. Intenta obtener el JetpackModule en el objeto tocado o sus padres
            JetpackModule jetpack = other.GetComponent<JetpackModule>();
            if (jetpack == null)
            {
                jetpack = other.GetComponentInParent<JetpackModule>();
            }
            // 2. Si no lo encuentra, lo busca en cualquier parte del objeto del jugador
            if (jetpack == null)
            {
                jetpack = other.transform.root.GetComponentInChildren<JetpackModule>();
            }

            if (jetpack != null)
            {
                // Forzamos la activación directa y la llamada al método
                jetpack.hasJetpack = true;
                jetpack.EnableJetpack();

                Debug.Log($"[ÉXITO] Jetpack activado correctamente en: {jetpack.gameObject.name}");
                
                // Destruye el objeto del suelo
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError($"[ERROR] {other.gameObject.name} colisionó con el Pickup, pero NO tiene el script 'JetpackModule' adjunto.");
            }
        }
    }
}