using UnityEngine;

public class JetpackModule : MonoBehaviour
{
    [Header("Estado")]
    public bool hasJetpack = false; // Se activa al recoger el pickup

    [Header("Configuración de Vuelo")]
    public KeyCode jetpackKey = KeyCode.Space; // Tecla para volar (Espacio)
    public float jetpackForce = 35f;           // Fuerza (Aumentada a 35)

    [Header("Combustible")]
    public float maxFuel = 100f;
    public float fuelBurnRate = 25f;
    public float fuelRefillRate = 15f;
    public float refillDelay = 1f;

    [Header("Estado Actual (Lectura)")]
    public float currentFuel;
    public bool isFlying;

    private Rigidbody rb;
    private CharacterController cc;
    private float cooldownTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        currentFuel = maxFuel;
    }

    void Update()
    {
        // Solo vuela si hasJetpack es verdadero y presionas la tecla
        if (hasJetpack && Input.GetKey(jetpackKey) && currentFuel > 0)
        {
            isFlying = true;
            cooldownTimer = refillDelay;
        }
        else
        {
            isFlying = false;
        }

        HandleFuel();

        // Si usas CharacterController (movimiento por código)
        if (isFlying && cc != null)
        {
            Vector3 moveDir = Vector3.up * jetpackForce * Time.deltaTime;
            cc.Move(moveDir);
        }
    }

    void FixedUpdate()
    {
        // Si usas Rigidbody (movimiento por físicas)
        if (isFlying && rb != null)
        {
            // Cancela la velocidad de caída antes de impulsar
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }

            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Acceleration);
        }
    }

    void HandleFuel()
    {
        if (isFlying)
        {
            currentFuel -= fuelBurnRate * Time.deltaTime;
            if (currentFuel <= 0)
            {
                currentFuel = 0;
                isFlying = false;
            }
        }
        else if (hasJetpack)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else if (currentFuel < maxFuel)
            {
                currentFuel += fuelRefillRate * Time.deltaTime;
                if (currentFuel > maxFuel) currentFuel = maxFuel;
            }
        }
    }

    public void EnableJetpack()
    {
        hasJetpack = true;
        currentFuel = maxFuel;
        Debug.Log(">>> EnableJetpack() ejecutado correctamente en " + gameObject.name + " <<<");
    }
}