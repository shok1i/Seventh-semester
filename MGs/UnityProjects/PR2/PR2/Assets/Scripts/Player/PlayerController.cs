using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")] 
    public float maxHp;
    public float moveSpeed = 5f;

    [Header("Weapon")] 
    public GameObject bulletPrefab;
    public float fireRate = 0.2f;
    public float bulletSpeed = 10f;
    
    private InputAction _fireInput;
    private InputAction _lookInput;
    private InputAction _moveInput;
    private PlayerInput _playerInput;

    private Rigidbody2D _rb;

    private float _curHp;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _curHp = maxHp;

        _playerInput = GetComponent<PlayerInput>();
        _moveInput = _playerInput.actions["Move"];
        _lookInput = _playerInput.actions["Look"];
        _fireInput = _playerInput.actions["Attack"];
        
        _fireInput.performed += OnShoot;
    }


    private void Update()
    {
        if (!GameManager.Instance.isPlayable) return;
        
        HandleMovement();
        HandleLook();
    }

    private void HandleMovement()
    {
        Vector2 movement = _moveInput.ReadValue<Vector2>() * moveSpeed;
        _rb.linearVelocity = movement;
    }

    private float _angle;

    private void HandleLook()
    {
        Vector2  mousePosition = _lookInput.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mousePosition.x, mousePosition.y, 0)
        );
        Vector2 direction = (mouseWorldPosition - transform.position);
        
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
    }

    private float _nextFire;
    private void OnShoot(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.isPlayable && Time.time >= _nextFire)
        {
            Shoot();
            _nextFire = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    
        Vector2 direction = new Vector2(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
        
        Destroy(bullet, 2f);
    }
    
    
    public void TakeDamage(int dmg)
    {
        _curHp -= dmg;
        if (_curHp <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.EndGame();
        }
    }
}