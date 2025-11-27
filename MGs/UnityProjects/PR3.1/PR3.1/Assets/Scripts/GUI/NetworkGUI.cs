using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGUI : NetworkBehaviour
{
    void OnGUI()
    {
        float w = 200f, h = 40f;
        float x = 10f, y = 10f;
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(x, y, w, h), "Host")) NetworkManager.Singleton.StartHost();
            if (GUI.Button(new Rect(x, y + h + 10, w, h), "Client")) NetworkManager.Singleton.StartClient();
        }
    }
}