using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float playerSpeed = 5;
    [SerializeField] private float playerRotationSpeed = 360;

    [SerializeField] private float positionRange = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        direction.Normalize();

        rb.linearVelocity = direction * playerSpeed;

        float rotationInput = 0f;
        
        if (Input.GetKey(KeyCode.Q))
            rotationInput = 1f;
        else if (Input.GetKey(KeyCode.E))
            rotationInput = -1f;

        if (rotationInput != 0f)
        {
            float rotationAmount = rotationInput * playerRotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(Random.Range(-positionRange, positionRange), 0, Random.Range(-positionRange, positionRange));
    }
}