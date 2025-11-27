using Unity.Netcode;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float fireRate = 0.2f;
    private float _nextFireTime = 0.25f;
    
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextFireTime)
        {
            GameObject go = Instantiate(projectile, transform.position + new Vector3(0f, .25f, 0f), transform.rotation);
            go.GetComponent<NetworkObject>().Spawn();
            _nextFireTime = Time.time + fireRate;
        }
    }
}
