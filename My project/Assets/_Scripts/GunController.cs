using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;
    private BoxCollider gunTrigger;
    public Guns gun;
    public LayerMask raycastLayerMask;
    public AudioSource audioSource;
    private bool canFire;
    private float nextTimeToFire;

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
    // Start is called before the first frame update
    void Start()
    {
        gunTrigger = GetComponent<BoxCollider>();
        canFire = true;
        SetTriggers();
    }
   
    IEnumerator CanFire()
    {
        canFire = false; 
        yield return new WaitForSeconds(gun.fireRate);
        canFire = true;
    }
    public void SetTriggers()
    {
        gunTrigger.size = new Vector3(gun.horizontalRange, gun.verticalRange, gun.range);
        gunTrigger.center = new Vector3(0,(0.5f * gun.verticalRange - 1f), gun.range * 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy)
        {
            EnemyManager.instance.AddEnemy(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            EnemyManager.instance.RemoveEnemy(enemy);

        }
    }

    public void Fire()
    {
        if (canFire)
        {
            audioSource.PlayOneShot(gun.sound);
            foreach (Enemy enemy in EnemyManager.instance.enemiesInRange)
            {
                var dir = (enemy.transform.position - transform.position).normalized;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, gun.range * 1.5f))
                {
                    if (hit.transform == enemy.transform)
                    {
                        Quaternion rot = Quaternion.LookRotation(-hit.normal);
                        enemy.Damage(gun.damage,rot);
                    }
                }
            }
            StartCoroutine(CanFire());
        }
    }
}
