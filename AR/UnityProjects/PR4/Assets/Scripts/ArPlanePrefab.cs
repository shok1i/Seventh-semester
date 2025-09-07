using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent((typeof(ARRaycastManager)))]
public class ArPlanePrefab : MonoBehaviour
{
    private ARRaycastManager _raycastManager;
    private GameObject _spawnedObject;
    private List<GameObject> _placedPrefabsList = new List<GameObject>();
    [SerializeField] private int maxPrefabsSpawnCount = 4;
    public GameObject placeablePrefab;
    static List<ARRaycastHit> _sHits = new List<ARRaycastHit>();
    
    private void Awake()
    {
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        
        touchPosition = default;
        return false;
    }
    
    void Update()
    {
        if (!TryGetTouchPosition(out var touchPosition)) return;
        if (_raycastManager.Raycast(touchPosition, _sHits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = _sHits[0].pose;
            if (_placedPrefabsList.Count < maxPrefabsSpawnCount) SpawnPrefab(hitPose);
        }
        
    }

    private void SpawnPrefab(Pose hitPose)
    {
        _spawnedObject = Instantiate(placeablePrefab, hitPose.position, hitPose.rotation);
        _placedPrefabsList.Add(_spawnedObject);
    }


    public TextMeshProUGUI tmpGUI;
    public void SetPrefabType(GameObject prefab)
    {
        placeablePrefab = prefab;
        tmpGUI.text = placeablePrefab.name;
    }
}
