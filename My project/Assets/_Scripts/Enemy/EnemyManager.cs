using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<Enemy> enemiesInRange = new List<Enemy>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemiesInRange.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInRange.Remove(enemy);
    }
}
