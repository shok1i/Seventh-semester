using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

public class AddObjectsToFace : MonoBehaviour
{
    [SerializeField] ARFace arFace;
    [SerializeField] int[] indexes =  {1, 14, 78, 292};
    [SerializeField] float pointerScale = 0.1f;
    [SerializeField] GameObject prefab = null;
    
    readonly Dictionary<int, Transform> _pointers = new Dictionary<int, Transform>();

    private void Awake()
    {
        arFace.updated += delegate
        {
            for (var i = 0; i < indexes.Length; i++)
            {
                var vertexindex = indexes[i];
                var pointer = getPointer(i);
                pointer.position = arFace.transform.TransformPoint(arFace.vertices[vertexindex]);
            }
        };
    }

    private Transform getPointer(int i)
    {
        if (_pointers.TryGetValue(i, out var pointer)) return pointer;
        else
        {
            var newPointer = createNewPointer();
            _pointers[i] = newPointer;
            return newPointer;
        }
    }

    private Transform createNewPointer()
    {
        var result = instatiatePointer();
        return result;
    }

    private Transform instatiatePointer()
    {
        if (prefab)
            return Instantiate(prefab).transform;
        else
        {
            var result = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            result.localScale = Vector3.one * pointerScale;
            return result;
        }
    }
}

