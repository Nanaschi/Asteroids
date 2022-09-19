using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : PoolerBase<Projectile>
{
    private PlayerInputActions _playerInputActions;
    private bool _thrusting;
    private float _turnDirection;
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private Projectile _laserPrefab;

    [SerializeField] private PlayerConfig _playerConfig;
    private GameObject _bulletPool;
    private GameObject _laserPool;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private float _fillPercent;
    private float _currentLaserBarFill;
    private int _currentLaserCharges;

    protected int CurrentLaserCharges
    {
        get => _currentLaserCharges;
        set
        {
            _currentLaserCharges = value;
            OnLaserChargeChanged?.Invoke(value);
        }
    }

    public PlayerConfig PlayerConfig => _playerConfig;

    public static event Action OnAsteroidCollided;
    public static event Action<Transform> OnTransformChanged;
    public static event Action<Rigidbody2D> OnActiveVelocity;

    public static event Func<float, float> OnLaserFilled;

    public static event Action<int> OnLaserChargeChanged;

    #region private properties

    private Vector3 TransformUp => transform.up;
    private bool Turning => _playerInputActions.Player.Movement.ReadValue<Vector2>().x != 0;

    private bool MovingForward => _playerInputActions.Player.Movement.ReadValue<Vector2>().y > 0;

    #endregion

    [Inject]
    public void InitInject(PlayerInputActions playerInputActions)
    {
        _playerInputActions = playerInputActions;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();


        InitBulletPool();
        InitLaserPool();
    }


    private void InitLaserPool()
    {
        InitPool(ref _bulletPool, projectilePrefab);
    }

    private void InitBulletPool()
    {
        InitPool(ref _laserPool, _laserPrefab);
    }

    private void InitPool(ref GameObject poolHolder, Projectile poolObject)
    {
        poolHolder = new GameObject(nameof(poolHolder));
        poolHolder.transform.SetParent(transform);
        InitPool(poolObject);
    }

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInputActions.Player.Shoot.performed += Shoot;
        Projectile.OnBoundaryReached += Release;
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Shoot.performed -= Shoot;
        Projectile.OnBoundaryReached -= Release;
    }

    #endregion

    private void FixedUpdate()
    {
        if (MovingForward) Movement();
        if (Turning) Turn();
        if (transform.hasChanged)
        {
            OnTransformChanged?.Invoke(transform);
            OnActiveVelocity?.Invoke(_rigidbody2D);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(FillLaserBar), 0, _fillSpeed);
    }

    private void FillLaserBar()
    {
        _currentLaserBarFill += _fillPercent;
        var barPercent = OnLaserFilled?.Invoke(_currentLaserBarFill);
        if (barPercent >= 1)
        {
            _currentLaserBarFill = 0;
            OnLaserFilled?.Invoke(_currentLaserBarFill);
            CurrentLaserCharges++;
        }
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


    protected override void GetSetup(Projectile projectile)
    {
        base.GetSetup(projectile);

        var bulletTransform = projectile.transform;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = transform.rotation;
        projectile.transform.SetParent(_bulletPool.transform);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Asteroid>())
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;

            gameObject.SetActive(false);

            OnAsteroidCollided?.Invoke();
        }
    }

    public class Factory : PlaceholderFactory<Player>
    {
        public override Player Create()
        {
            var player = base.Create();
            player.CurrentLaserCharges = 0;
            return player;
        }
    }
}