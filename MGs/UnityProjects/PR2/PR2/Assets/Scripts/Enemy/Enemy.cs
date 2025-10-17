using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 2f;
    public float attackDistance = 1.2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public int health = 30;

    public event Action<Enemy> OnDeath;

    private Rigidbody2D rb;
    private float nextAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy) return;

        Vector2 dir = (target.position - transform.position).normalized;
        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > attackDistance)
        {
            rb.MovePosition(rb.position + dir * speed * Time.deltaTime);
        }
        else if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            Attack();
        }
    }

    void Attack()
    {
        // Простая атака — наносим урон игроку
        var player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }
}