using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _rotateInput;
    private Rigidbody2D _rigidbody;


    private Character _selectedCharacter;
    private Weapon _weapon;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _selectedCharacter = GetComponent<PlayerLogic>().selectedCharacter;
        _weapon = _selectedCharacter.startWeapon;
    }

    // = = = = = = = = = = 
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        Debug.Log($"Click");
    }

    public void OnRotate(InputValue value)
    {
        _rotateInput = value.Get<float>();
    }
    
    
    // Переделать под зажатие + нажатие
    private float _nextFireTime = 0f;
    public float spawnOffset = .75f;
    public void OnShoot(InputValue value)
    {
        if (!value.isPressed) return;

        if (Time.time < _nextFireTime) return;
        _nextFireTime = Time.time + _weapon.fireRate;
        
        Vector3 spawnPos = transform.position + transform.up * spawnOffset;
        
        GameObject bullet = Instantiate(_weapon.bulletPrefab, spawnPos, transform.rotation);
        bullet.gameObject.GetComponent<BulletLogic>().weapon = _weapon;
    }

    // = = = = = = = = = = 
    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _moveInput * _selectedCharacter.maxSpeed;

        if (Math.Abs(_rotateInput) > 0.01f)
            _rigidbody.MoveRotation(_rigidbody.rotation + _rotateInput * 100 * Time.fixedDeltaTime);
    }

    // = = = = = = = = = = 
}