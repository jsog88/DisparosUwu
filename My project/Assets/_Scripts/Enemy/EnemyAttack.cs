
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAggro enemyAggro;
    [HideInInspector] public bool isAttacking;

    private void Start()
    {
        enemyAggro = GetComponentInParent<EnemyAggro>();
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //enemyAggro.animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //enemyAggro.animator.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }

}
