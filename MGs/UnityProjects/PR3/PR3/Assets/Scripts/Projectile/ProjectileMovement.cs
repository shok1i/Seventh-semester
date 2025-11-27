using System;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float shootForce = 20f;
    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _rb.transform.up * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
