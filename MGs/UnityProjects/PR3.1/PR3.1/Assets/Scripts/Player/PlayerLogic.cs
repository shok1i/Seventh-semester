using Unity.Netcode;
using UnityEngine;

public class PlayerLogic : NetworkBehaviour
{
    private float _maxHp = 100;
    [SerializeField] public float currentHp;
    [SerializeField] private float positionRange = 5f;
    
    public int lastHit;
    
    void Start()
    {
        currentHp = _maxHp;
    }

    void Update()
    {
        if (currentHp <= 0)
        {
            Debug.Log($"{gameObject.name} dead!");
        
            if (IsServer)
            {
                ScoreLogic.Instance.ChangeScoreClientRpc(lastHit);
                RespawnClientRpc();
            }
        }
    }

    [ClientRpc]
    private void RespawnClientRpc()
    {
        currentHp = _maxHp;
        transform.position = new Vector3(Random.Range(-positionRange, positionRange), 0, Random.Range(-positionRange, positionRange));
    }
}