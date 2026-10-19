using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Shooter/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Información General")]
    public string weaponName;
    public GameObject weaponPrefab; // El Prefab visual 3D que irá en la mano del jugador

    [Header("Estadísticas")]
    public int maxAmmo = 30;
    public float fireRate = 0.1f;
    public float damage = 10f;
}