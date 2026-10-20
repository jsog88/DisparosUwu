using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Salud")]
    public int maxHealth = 10;
    [HideInInspector] public int health;
    public Scrollbar healthScrollbar;

    [Header("Armadura")]
    public bool hasArmor = false;
    public int maxArmor = 10;
    public int currentArmor;
    public Scrollbar armorScrollbar;

    void Start()
    {
        health = maxHealth;
        currentArmor = 0;

        if (armorScrollbar != null)
        {
            armorScrollbar.gameObject.SetActive(false);
        }

        UpdateUI();
    }

    public void PlayerDamage()
    {
        if (hasArmor && currentArmor > 0)
        {
            currentArmor--;
            Debug.Log("Daño en Armadura: " + currentArmor);

            if (currentArmor <= 0)
            {
                currentArmor = 0;
                hasArmor = false;
            }
        }
        else
        {
            if (health > 1)
            {
                health--;
                Debug.Log("Salud actual: " + health);
            }
            else if (health <= 1)
            {
                health = 0;
                Debug.Log("Personaje Muere. Reiniciando nivel...");
                UpdateUI();

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
        }

        UpdateUI();
    }

    public bool TryHeal(int amount)
    {
        if (health >= maxHealth)
        {
            Debug.Log("La salud ya está al máximo.");
            return false;
        }

        health = Mathf.Min(health + amount, maxHealth);
        Debug.Log($"Jugador curado. Salud actual: {health} / {maxHealth}");

        UpdateUI();
        return true;
    }

    public void EnableArmor()
    {
        hasArmor = true;
        currentArmor = maxArmor;

        if (armorScrollbar != null)
        {
            armorScrollbar.gameObject.SetActive(true);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (healthScrollbar != null && maxHealth > 0)
        {
            healthScrollbar.value = 0f;
            healthScrollbar.size = (float)health / (float)maxHealth;
        }

        if (armorScrollbar != null)
        {
            if (hasArmor && currentArmor > 0)
            {
                if (!armorScrollbar.gameObject.activeSelf)
                    armorScrollbar.gameObject.SetActive(true);

                armorScrollbar.value = 0f;
                armorScrollbar.size = (float)currentArmor / (float)maxArmor;
            }
            else
            {
                if (armorScrollbar.gameObject.activeSelf)
                    armorScrollbar.gameObject.SetActive(false);
            }
        }
    }
}