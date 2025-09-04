using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracking : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabsList = new List<GameObject>();
    private ARTrackedImageManager _trackedImage;
    private Dictionary<string, GameObject> _arObjects;

    private void Start()
    {
        _trackedImage = GetComponent<ARTrackedImageManager>();
        if (_trackedImage == null) return;
        _trackedImage.trackablesChanged.AddListener(OnImagesChanged);
        _arObjects = new Dictionary<string, GameObject>();
        SetupSceneElements();
    }

    private void OnDestroy()
    {
        _trackedImage.trackablesChanged.RemoveListener(OnImagesChanged);
    }

    private void SetupSceneElements()
    {
        foreach (var prefab in prefabsList)
        {
            var arObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            arObject.name = prefab.name;
            arObject.gameObject.SetActive(false);
            _arObjects.Add(arObject.name, arObject);
        }
    }

    private void OnImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
            UpdateTrackedImage(trackedImage);
        
        foreach (var trackedImage in eventArgs.updated)
            UpdateTrackedImage(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
            UpdateTrackedImage(trackedImage.Value);
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        if(trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }
        
        _arObjects[trackedImage.referenceImage.name].gameObject.SetActive(true);
        _arObjects[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        _arObjects[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}
