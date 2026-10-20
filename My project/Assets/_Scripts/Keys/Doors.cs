using UnityEngine;

public class Doors : MonoBehaviour
{
    [Tooltip("0 Púrpura, 1 Roja, 2 Azul")]
    public int keyID;

    private bool hasKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GotKey();

            if (hasKey)
            {
                // Consumir/ocultar la llave del HUD al usarla para abrir la puerta
                UseKey();

                Debug.Log($"¡Puerta utilizada con la llave {keyID}! Destruyendo puerta...");
                
                // Destruye el GameObject de la puerta en lugar de hacer animación
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Necesitas la llave tipo {keyID} para abrir esta puerta.");
            }
        }
    }

    private void GotKey()
    {
        switch (keyID)
        {
            case 0:
                hasKey = Keys.hasPurpleKey;
                break;
            case 1:
                hasKey = Keys.hasRedKey;
                break;
            case 2:
                hasKey = Keys.hasBlueKey;
                break;
            default:
                hasKey = false;
                break;
        }
    }

    private void UseKey()
    {
        // Resetea la variable estática y oculta el ícono del HUD
        switch (keyID)
        {
            case 0:
                Keys.hasPurpleKey = false;
                if (KeyManager.Instance != null && KeyManager.Instance.purpleKeyImage != null)
                    KeyManager.Instance.purpleKeyImage.SetActive(false);
                break;
            case 1:
                Keys.hasRedKey = false;
                if (KeyManager.Instance != null && KeyManager.Instance.redKeyImage != null)
                    KeyManager.Instance.redKeyImage.SetActive(false);
                break;
            case 2:
                Keys.hasBlueKey = false;
                if (KeyManager.Instance != null && KeyManager.Instance.blueKeyImage != null)
                    KeyManager.Instance.blueKeyImage.SetActive(false);
                break;
        }
    }
}