using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacter",  menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{
    [Header("Основные параметры")]
    public string characterName;
    
    public GameObject characterPrefab;
    
    public float maxSpeed;
    public float maxHealth;

    [Header("Начальное оружие")] 
    public Weapon startWeapon;
}
