using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}