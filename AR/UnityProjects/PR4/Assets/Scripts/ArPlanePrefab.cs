using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent((typeof(ARRaycastManager)))]
public class ArPlanePrefab : MonoBehaviour
{
    static List<ARRaycastHit> s_hits = new List<ARRaycastHit>();
    private ARRaycastManager raycastManager;
    public GameObject PlaceablePrefab;
    private GameObject spawnedPlane;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 position)
    {
        if (Input.touchCount > 0)
        {
            position = Input.GetTouch(0).position;
            return true;
        }
        position = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out var touchPosition)) return;

        if (raycastManager.Raycast(touchPosition, s_hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = s_hits[0].pose;
            if (spawnedPlane == null)
            {
                spawnedPlane = Instantiate(PlaceablePrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedPlane.transform.position = hitPose.position;
                spawnedPlane.transform.rotation = hitPose.rotation;
            }
        }
        
    }
}
