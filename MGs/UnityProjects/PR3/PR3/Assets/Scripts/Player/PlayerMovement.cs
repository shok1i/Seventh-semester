using Unity.Netcode;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float rotationSpeed = 5000f;
    [SerializeField] private float positionRange = 10f;

    void Update()
    {
        if (!IsOwner) return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, vertical, 0f);
        movementDirection.Normalize();

        transform.Translate(movementDirection * movementSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(0, 0, - rotationSpeed * Time.deltaTime);
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(Random.Range(-positionRange, positionRange), 0, Random.Range(-positionRange, positionRange));
    }
}