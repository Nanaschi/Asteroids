using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PoolerBase<Bullet> //TODO inject it
{
    private PlayerInputActions _playerInputActions; //TODO transfer into DI
    private bool _thrusting;
    private float _turnDirection;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed; //TODO make it a SO model
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Bullet _bullet;
    [SerializeField] GameObject _bulletPool;

    #region private properties

    private Vector3 TransformUp => transform.up;
    private bool Turning => _playerInputActions.Player.Movement.ReadValue<Vector2>().x != 0;

    private bool MovingForward => _playerInputActions.Player.Movement.ReadValue<Vector2>().y > 0;

    #endregion

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();

        InitPool(_bullet);
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
        _rigidbody2D.AddTorque(-readVector2PlayerMovement.x * _turnSpeed);
    }

    private void Movement()
    {
        _rigidbody2D.AddForce(TransformUp * _movementSpeed);
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
        bullet.OnBulletCollided += Release;
    }

    protected override void ReleaseSetup(Bullet obj)
    {
        base.ReleaseSetup(obj);

        obj.OnBulletCollided -= Release;
    }
}