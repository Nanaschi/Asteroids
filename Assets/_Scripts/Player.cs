using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : PoolerBase<Bullet> //TODO inject it
{
    private PlayerInputActions _playerInputActions; //TODO transfer into DI
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] [Tooltip("That's where bullets are placed")]
    private GameObject _bulletPool; //TODO: Instantiate it in real time 

    [SerializeField] private PlayerConfig _playerConfig;

    public PlayerConfig PlayerConfig => _playerConfig;

    #region private properties

    private Vector3 TransformUp => transform.up;
    private bool Turning => _playerInputActions.Player.Movement.ReadValue<Vector2>().x != 0;

    private bool MovingForward => _playerInputActions.Player.Movement.ReadValue<Vector2>().y > 0;

    #endregion

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        _rigidbody2D = GetComponent<Rigidbody2D>();
        InitPool(_bulletPrefab);
    }

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInputActions.Player.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Shoot.performed -= Shoot;
    }

    #endregion

    private void FixedUpdate()
    {
        if (MovingForward) Movement();
        if (Turning) Turn();
    }

    private void Turn()
    {
        var readVector2PlayerMovement = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody2D.AddTorque(-readVector2PlayerMovement.x * _playerConfig.TurnSpeed);
    }

    private void Movement()
    {
        _rigidbody2D.AddForce(TransformUp * _playerConfig.MovementSpeed);
    }


    private void Shoot(InputAction.CallbackContext callbackContext)
    {
        var bullet = Get();

        bullet.Project(TransformUp);
    }


    protected override void GetSetup(Bullet bullet)
    {
        base.GetSetup(bullet);

        var bulletTransform = bullet.transform;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = transform.rotation;
        bullet.transform.SetParent(_bulletPool.transform);
        bullet.OnBoundaryReached += Release;
    }

    protected override void ReleaseSetup(Bullet obj)
    {
        base.ReleaseSetup(obj);

        obj.OnBoundaryReached -= Release;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Asteroid>())
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;
            
            gameObject.SetActive(false);
            
            //TODO: transform into event OnAsteroidCollided
            FindObjectOfType<GameManager>().PlayerDied(); //TODO: Change into DI
            
        }
    }
}