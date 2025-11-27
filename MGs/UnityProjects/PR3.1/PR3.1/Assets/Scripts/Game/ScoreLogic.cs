using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLogic : NetworkBehaviour
{
    public static ScoreLogic Instance;

    private void Start()
    {
        Instance = this;
    }

    [SerializeField] private TextMeshProUGUI scoreText;

    [System.Serializable]
    public struct ScoreData : INetworkSerializable
    {
        public FixedString32Bytes name;
        public int score;

        public ScoreData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref name);
            serializer.SerializeValue(ref score);
        }
    }

    private List<ScoreData> scoreData = new List<ScoreData>();

    [ClientRpc] 
    public void ChangeScoreClientRpc(int playerId)
    {
        var data = scoreData[playerId];
        data.score++;
        scoreData[playerId] = data;

        UpdateScoreTextClientRpc();

        if (scoreData[playerId].score >= 5)
        {
            WinMenuClientRpc(scoreData[playerId]);
        }
    }

    private void Update()
    {
        if (scoreData.Count != 0) UpdateScoreTextClientRpc();
    }

    public void AddPlayer(int playerId)
    {
        scoreData.Add(new ScoreData($"Player {playerId + 1}", 0));
        UpdateScoreTextClientRpc();
        Debug.Log($"scoreDataAdd {scoreData.Count}, {playerId}");
    }

    [ClientRpc] 
    private void UpdateScoreTextClientRpc()
    {
        scoreText.text = "";
        foreach (ScoreData data in scoreData)
        {
            scoreText.text += data.name + ": " + data.score + "\n";
        }
    }

    [SerializeField] private GameObject winMenu;
    [SerializeField] private TextMeshProUGUI winerName;

    [SerializeField] private GameObject btn;
    
    [ClientRpc] 
    private void WinMenuClientRpc(ScoreData winer)
    {
        winMenu.SetActive(true);
        winerName.text = winer.name.ToString();
        if (!IsServer) btn.SetActive(false);
    }

    [ClientRpc] 
    public void RestartGameClientRpc()
    {
        for (int i = 0; i < scoreData.Count; i++)
        {
            var data = scoreData[i];
            data.score = 0;
            scoreData[i] = data;
        }
        
        UpdateScoreTextClientRpc();
        winMenu.SetActive(false);
    }
}