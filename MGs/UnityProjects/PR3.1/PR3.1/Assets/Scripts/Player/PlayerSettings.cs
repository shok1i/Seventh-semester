using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerЫSettings : NetworkBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMeshProUGUI playerName;

    [SerializeField] private NetworkVariable<FixedString128Bytes> playerNameBytes =
        new NetworkVariable<FixedString128Bytes>("Player: 0", NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Server);

    public List<Color> colors = new List<Color>();

    public override void OnNetworkSpawn()
    {
        playerNameBytes.Value = "Player: " + (OwnerClientId + 1);
        playerName.text = playerNameBytes.Value.ToString();
        meshRenderer.material.color = colors[(int)OwnerClientId];
        ScoreLogic.Instance.AddPlayer((int)OwnerClientId);
    }
}