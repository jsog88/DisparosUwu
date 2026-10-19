using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/New Weapon")]
public class Guns : ScriptableObject
{
    public string weaponName; // <-- AGREGA ESTA LÍNEA
    public GameObject weaponPrefab;
    public float range;
    public int verticalRange;
    public int horizontalRange;
    public float fireRate;
    public int damage;
    public int maxAmmo = 30;
    public AudioClip sound;
}