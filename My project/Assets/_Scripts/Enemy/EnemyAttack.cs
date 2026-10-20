using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAggro enemyAggro;
    [HideInInspector] public bool isAttacking;

    [Header("Ataque")]
    public float attackCooldown = 1f; // Tiempo en segundos entre cada golpe
    private float nextAttackTime = 0f;

    private void Start()
    {
        enemyAggro = GetComponentInParent<EnemyAggro>();
        isAttacking = false;
    }

   private void OnTriggerEnter(Collider other)
{
    // Si la colisión vino de la zona de disparo (GunTriggerZone) o un Trigger, la ignora:
    if (other.gameObject.name == "GunTriggerZone") return;

    // Aquí continúa tu código normal de detección del Jugador...
    if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            isAttacking = true;

            // Hace daño de inmediato al tocarlo
            ApplyDamage(other);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Sigue haciendo daño cada cierto tiempo mientras el jugador permanezca dentro
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                ApplyDamage(other);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    private void ApplyDamage(Collider playerCollider)
    {
        // Busca el script PlayerStats en el jugador
        PlayerStats stats = playerCollider.GetComponent<PlayerStats>();
        if (stats == null)
        {
            stats = playerCollider.GetComponentInParent<PlayerStats>();
        }

        if (stats != null)
        {
            stats.PlayerDamage(); // Llama directamente al daño de PlayerStats
        }
    }
}