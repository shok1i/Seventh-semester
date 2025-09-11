using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxEffect : MonoBehaviour
{
    [System.Serializable]
    public class Layer
    {
        public String name = "";
        public RectTransform  layer;
        public float multiplier;
        [HideInInspector] public Vector3 startPos;
    }

    public Layer[] layers;
    public float smooth = 5f;

    private Vector3 _screenCenter;

    private void Start()
    {
        _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        // Debug.Log("ScreenCenter: " + _screenCenter);
        foreach (Layer layer in layers)
            layer.startPos = layer.layer.anchoredPosition;
    }

    private void Update()
    {
        Vector3 rowPos = Mouse.current.position.ReadValue();
        Vector3 mousePosition = new Vector3(
            Math.Clamp(rowPos.x, 0, Screen.width),
            Math.Clamp(rowPos.y, 0, Screen.width),
            0
        );

        
        Vector3 offset = new Vector3(
            (mousePosition.x - _screenCenter.x) / _screenCenter.x,
            (mousePosition.y - _screenCenter.y) / _screenCenter.y,
            0f
        );
        
        // Debug.Log($"mousePosition: {mousePosition}, offset: {offset}");

        
        foreach (Layer layer in layers)
            if (layer.layer)
            {
                Vector3 target = layer.startPos + (offset * layer.multiplier);

                layer.layer.anchoredPosition =
                    Vector3.Lerp(layer.layer.anchoredPosition, target, smooth * Time.deltaTime);
            }
    }
}