using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : MonoBehaviour
{
    [SerializeField] private float damageInterval = 0.5f;
    [SerializeField] private int damageAmount = 10;
    
    private class PlayerDamageInfo
    {
        public GameObject player;
        public Coroutine damageCoroutine;
        public bool isOutside;
    }
    
    private Dictionary<GameObject, PlayerDamageInfo> playerInfos = new Dictionary<GameObject, PlayerDamageInfo>();
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            
            if (!playerInfos.TryGetValue(player, out PlayerDamageInfo info))
            {
                info = new PlayerDamageInfo { player = player };
                playerInfos[player] = info;
            }
            
            info.isOutside = true;
            
            if (info.damageCoroutine == null)
                info.damageCoroutine = StartCoroutine(DamageOverTime(info));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = other.gameObject;
            
            if (playerInfos.TryGetValue(player, out PlayerDamageInfo info))
            {
                info.isOutside = false;
                
                if (info.damageCoroutine != null)
                {
                    StopCoroutine(info.damageCoroutine);
                    info.damageCoroutine = null;
                }
            }
        }
    }
    
    private IEnumerator DamageOverTime(PlayerDamageInfo info)
    {
        while (info.isOutside && info.player != null)
        {
            yield return new WaitForSeconds(damageInterval);
            
            if (info.isOutside && info.player != null)
                ApplyDamage(info.player);
        }
        
        info.damageCoroutine = null;
    }
    
    private void ApplyDamage(GameObject player)
    {
        Debug.Log($"Нанесен урон: {damageAmount} игроку {player.name}");
    }
    
    private void OnDestroy()
    {
        foreach (var info in playerInfos.Values)
            if (info.damageCoroutine != null)
                StopCoroutine(info.damageCoroutine);
        playerInfos.Clear();
    }
    
    private void Update()
    {
        var destroyedPlayers = new List<GameObject>();
        foreach (var kvp in playerInfos)
            if (kvp.Key == null)
                destroyedPlayers.Add(kvp.Key);
        
        foreach (var player in destroyedPlayers)
            playerInfos.Remove(player);
    }
}