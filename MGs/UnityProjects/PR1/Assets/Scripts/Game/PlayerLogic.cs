using System;
using TMPro;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    private float _maxHp;
    public float currentHp;
    
    public TextMeshProUGUI hpText;

    public Character selectedCharacter;
    private RespawnLogic respawnLogic;

    private int maxSpawn = 1;

    private void Awake()
    {
        Debug.Log($"Initializing player:  {gameObject.name}");
        
        _maxHp = selectedCharacter.maxHealth;
        currentHp = _maxHp;
        
        respawnLogic = FindObjectOfType<RespawnLogic>();
    }

    private void FixedUpdate()
    {
        hpText.text = $"Lives: {maxSpawn}";
        
        if (currentHp < 0)
        {
            maxSpawn--;
            respawnLogic.Respawn(gameObject);
            currentHp = _maxHp;
        }
        
        if (maxSpawn <= 0)
        {
            hpText.text = $"You dead!";
            Destroy(gameObject);
        }
    }
}
