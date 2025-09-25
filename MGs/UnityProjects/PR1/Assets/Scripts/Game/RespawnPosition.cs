using System;
using UnityEngine;

public class RespawnPosition : MonoBehaviour
{
    public bool state = true;
    public float preventSpawnRadius;

    private void Awake()
    {
        state = true;
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.CircleCast(transform.position, preventSpawnRadius, Vector2.zero, 0);

        if (hit.collider.gameObject.CompareTag("Player"))
        {
            state = false;
        }
        else
            state = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellowGreen;
        Gizmos.DrawWireSphere(transform.position, preventSpawnRadius);
    }
}