using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneLogic : MonoBehaviour
{
    [SerializeField] public Transform[] zonePositions;
    [SerializeField] public GameObject playableZone;
    [SerializeField] public GameObject unPlayableZone;

    private Vector3 _minPlayableZone = new Vector3(5, 5, 5);

    void Start()
    {
        transform.position = zonePositions[Random.Range(1, zonePositions.Length)].position;
        playableZone.transform.localScale = new Vector3(30, 30, 30);
    }

    float _tempTime = 0f;

    void Update()
    {
        _tempTime += Time.deltaTime;

        if (_tempTime >= 5f)
            StartCoroutine(ScaleCoroutine(playableZone.transform, _minPlayableZone, 60));

        if (_tempTime >= 80f)
            StartCoroutine(ScaleCoroutine(playableZone.transform, new Vector3(0, 0, 0), 20));
    }

    private IEnumerator ScaleCoroutine(Transform target, Vector3 targetScale, float duration)
    {
        Vector3 startScale = target.localScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;
            target.localScale = Vector3.Lerp(startScale, targetScale, progress);
            yield return null;
        }

        target.localScale = targetScale;
    }
}