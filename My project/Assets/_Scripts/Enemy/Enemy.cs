using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 10f;
    public GameObject blood;


    // Update is called once per frame
    void Update()
    {
        EnemyDead();
    }
    public void Damage(int damage, Quaternion rot)
    {
        health -= damage;
        GameObject bloodIntance = Instantiate(blood, transform.position, rot);
        Destroy(bloodIntance, 2);
    
    }
    public void EnemyDead()
    {
        if (health <= 0)
        {
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
