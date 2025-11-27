using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float shootForce = 20f;
    private Rigidbody _rb;

    public int shooterId = 0;

    public override void OnNetworkSpawn()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.linearVelocity = transform.forward * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerLogic = other.gameObject.GetComponent<PlayerLogic>();
            if (playerLogic != null)
            {
                playerLogic.currentHp -= 50;
                playerLogic.lastHit = shooterId;
                Debug.Log($"shooterId: {shooterId}");
            }
        }

        DestroyBulletServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyBulletServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}