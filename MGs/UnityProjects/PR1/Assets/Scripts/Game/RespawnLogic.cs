using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RespawnLogic : MonoBehaviour
{
    public RespawnPosition[] respawnPositions;
    public GameObject[] playersPrefab;

    public GameObject path;

    private void Start()
    {
        Debug.Log($"Players count: {PlayerCount.Number}");

        for (int i = 0; i < PlayerCount.Number; i++)
        {
            var availablePositions = respawnPositions.Where(rp => rp.state).ToList();
            if (availablePositions.Count == 0) return;
        
            RespawnPosition chosen = availablePositions[Random.Range(0, availablePositions.Count)];
            chosen.state = false;

            Debug.Log(playersPrefab[i].transform.name);
            Instantiate(playersPrefab[i], chosen.transform.position, chosen.transform.rotation, path.transform);
        }
    }
    
    public void Respawn(GameObject player)
    {
        var availablePositions = respawnPositions.Where(rp => rp.state).ToList();
        if (availablePositions.Count == 0) return;
        
        RespawnPosition chosen = availablePositions[Random.Range(0, availablePositions.Count)];
        player.transform.position = chosen.transform.position;
    }
}