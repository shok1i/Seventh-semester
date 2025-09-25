using System;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public Animator animator;
    
    public Weapon weapon;


    private void FixedUpdate()
    {
        transform.position += transform.up * weapon.bulletSpeed;
        Destroy(gameObject, weapon.lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject hit = other.gameObject;

        Debug.Log($"Hit {hit.name}");
        
        if (hit.tag == "Player")
            hit.GetComponent<PlayerLogic>().currentHp -= weapon.maxDamage;
        
        if (hit.tag != "Bullet")
            Destroy(gameObject);
    }
}
