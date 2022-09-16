using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : PoolerBase<Bullet>
{
    private PlayerInputActions _playerInputActions; //TODO transfer into DI
    private bool _thrusting;
    private float _turnDirection;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed; //TODO make it a SO model
    [SerializeField] private float _turnSpeed;
    [SerializeField] private Bullet _bullet;

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
        var readVector2PlayerMovement = _playerInputActions.Player.Movement.ReadValue<Vector2>();
        _rigidbody2D.AddForce(TransformUp * _movementSpeed);
    }


    private void Shoot(InputAction.CallbackContext callbackContext)
    {
        var bullet = Get();

        bullet.Project(TransformUp);
    }


    protected override void GetSetup(Bullet obj)
    {
        base.GetSetup(obj);
        
        var bulletTransform = obj.transform;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = transform.rotation;
        obj.OnBulletCollided += Release;
    }

    protected override void ReleaseSetup(Bullet obj)
    {
        base.ReleaseSetup(obj);
        
        obj.OnBulletCollided -= Release;
    }
    /* private void MovementPerformed(InputAction.CallbackContext callbackContext)
     {
         readVector2 = callbackContext.ReadValue<Vector2>();
         _thrusting = readVector2.y > 0;
         _turnDirection = 0;
         if (readVector2.x < 0)
         {
             _turnDirection = 1f; print("Left");
         }
         else if (readVector2.x > 0)
         {
             _turnDirection = -1f; print("Right");
         }
         
     }*/
}