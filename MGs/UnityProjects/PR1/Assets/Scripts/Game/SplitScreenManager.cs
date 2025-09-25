using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class SplitScreenManager : MonoBehaviour
{
    private List<PlayerInput> players = new List<PlayerInput>();

    private void OnEnable()
    {
        var pim = GetComponent<PlayerInputManager>();
        pim.onPlayerJoined += OnPlayerJoined;
        pim.onPlayerLeft += OnPlayerLeft; // Работает начиная с Unity 2022+
    }

    private void OnDisable()
    {
        var pim = GetComponent<PlayerInputManager>();
        pim.onPlayerJoined -= OnPlayerJoined;
        pim.onPlayerLeft -= OnPlayerLeft;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        players.Add(playerInput);
        UpdateCameras();
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        players.Remove(playerInput);
        UpdateCameras();
    }

    private void UpdateCameras()
    {
        int count = players.Count;

        for (int i = 0; i < players.Count; i++)
        {
            Camera cam = players[i].GetComponentInChildren<Camera>();
            if (cam == null) continue;

            switch (count)
            {
                case 1:
                    cam.rect = new Rect(0f, 0f, 1f, 1f);
                    break;

                case 2:
                    cam.rect = (i == 0)
                        ? new Rect(0f, 0.5f, 1f, 0.5f) // верх
                        : new Rect(0f, 0f, 1f, 0.5f);   // низ
                    break;

                case 3:
                    if (i == 0)
                        cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f); // верх левый
                    else if (i == 1)
                        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // верх правый
                    else
                        cam.rect = new Rect(0f, 0f, 1f, 0.5f);       // низ
                    break;

                case 4:
                    if (i == 0)
                        cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f); // верх левый
                    else if (i == 1)
                        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f); // верх правый
                    else if (i == 2)
                        cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);   // низ левый
                    else
                        cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f); // низ правый
                    break;
            }
        }
    }
}
