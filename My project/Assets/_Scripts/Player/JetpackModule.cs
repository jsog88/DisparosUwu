using UnityEngine;
using UnityEngine.UI;

public class JetpackModule : MonoBehaviour
{
    [Header("Estado")]
    public bool hasJetpack = false;

    [Header("UI / HUD")]
    public Scrollbar fuelScrollbar;

    [Header("Configuración de Vuelo")]
    public KeyCode jetpackKey = KeyCode.Space;
    public float jetpackForce = 35f;

    [Header("Combustible")]
    public float maxFuel = 100f;
    public float fuelBurnRate = 25f; // Consumo de gasolina por segundo

    [Header("Estado Actual")]
    public float currentFuel;
    public bool isFlying;

    private Rigidbody rb;
    private CharacterController cc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        currentFuel = maxFuel;

        if (fuelScrollbar != null)
        {
            fuelScrollbar.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (hasJetpack && Input.GetKey(jetpackKey) && currentFuel > 0)
        {
            isFlying = true;
        }
        else
        {
            isFlying = false;
        }

        HandleFuel();
        UpdateUI();

        if (isFlying && cc != null)
        {
            Vector3 moveDir = Vector3.up * jetpackForce * Time.deltaTime;
            cc.Move(moveDir);
        }
    }

    void FixedUpdate()
    {
        if (isFlying && rb != null)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
        }
    }

    void HandleFuel()
    {
        // Solo consume gasolina mientras vuela
        if (isFlying)
        {
            currentFuel -= fuelBurnRate * Time.deltaTime;
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                isFlying = false;
                hasJetpack = false; // Se destruye/desactiva el jetpack al agotarse
            }
        }
    }

    void UpdateUI()
    {
        if (fuelScrollbar == null) return;

        if (hasJetpack && currentFuel > 0)
        {
            if (!fuelScrollbar.gameObject.activeSelf)
            {
                fuelScrollbar.gameObject.SetActive(true);
            }

            fuelScrollbar.size = currentFuel / maxFuel;
        }
        else
        {
            if (fuelScrollbar.gameObject.activeSelf)
            {
                fuelScrollbar.gameObject.SetActive(false);
            }
        }
    }

    public void EnableJetpack()
    {
        hasJetpack = true;
        currentFuel = maxFuel;

        if (fuelScrollbar != null)
        {
            fuelScrollbar.gameObject.SetActive(true);
            fuelScrollbar.size = 1f;
            fuelScrollbar.value = 1f;
        }
    }
}