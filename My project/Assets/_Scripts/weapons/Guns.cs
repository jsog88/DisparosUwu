using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class Guns : ScriptableObject
{
    public float range;
    public int verticalRange;
    public int horizontalRange;
    public float fireRate;
    public int damage;
    public AudioClip sound;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
