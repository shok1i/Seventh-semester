using Unity.Netcode;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate = 0.2f;
    private float _nextFireTime = 0.5f;

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFireTime)
        {
            ShootServerRpc(spawnPoint.position, transform.rotation);
            _nextFireTime = Time.time + fireRate;
        }
    }

    [ServerRpc]
    private void ShootServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(projectile, position, rotation);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.shooterId = (int)OwnerClientId;
        
        NetworkObject networkObject = bullet.GetComponent<NetworkObject>();
        networkObject.Spawn();
        
        SetBulletShooterClientRpc(networkObject.NetworkObjectId, (int)OwnerClientId);
    }

    [ClientRpc]
    private void SetBulletShooterClientRpc(ulong networkObjectId, int shooterId)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(networkObjectId, out NetworkObject networkObject))
        {
            Bullet bullet = networkObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.shooterId = shooterId;
            }
        }
    }
}