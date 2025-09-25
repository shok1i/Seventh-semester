using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon",  menuName = "ScriptableObjects/Weapon")]

public class Weapon : ScriptableObject
{
    [Header("Основные параметры")]
    public string weaponName;
    
    public GameObject bulletPrefab;
    
    public Sprite sprite;
    
    public float bulletSpeed;
    public float maxDamage;
    public float fireRate;
    public float lifetime;

    public int ammo;
}
