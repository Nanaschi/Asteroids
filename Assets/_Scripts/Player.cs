using System;
using _Scripts.FlyingObjects;
using _Scripts.Projectiles;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : DoublePoolerBase<Projectile, Projectile>
    {
        private PlayerInputActions _playerInputActions;
        private bool _thrusting;
        private float _turnDirection;
        private Rigidbody2D _rigidbody2D;



        [SerializeField] private PlayerConfig _playerConfig;
        private GameObject _bulletPool;
        private GameObject _laserPool;

        private float _currentLaserBarFill;
        private int _currentLaserCharges;

        private int CurrentLaserCharges
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


            InitPool(_playerConfig.BulletPrefab, _playerConfig.LaserPrefab);
        }

        #region Enable/Disable

        private void OnEnable()
        {
            _playerInputActions.Player.ShootPrimary.performed += ShootPrimary;
            _playerInputActions.Player.ShootSecondary.performed += ShootSecondary;
            Bullet.OnBoundaryReached += Release1;
            Laser.OnBoundaryReached += Release2;
        }

        private void OnDisable()
        {
            _playerInputActions.Player.ShootPrimary.performed -= ShootPrimary;
            _playerInputActions.Player.ShootSecondary.performed -= ShootSecondary;
            Bullet.OnBoundaryReached -= Release1;
            Laser.OnBoundaryReached -= Release2;
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
            InvokeRepeating(nameof(FillLaserBar), 0, _playerConfig.FillSpeed);
        }

        private void FillLaserBar()
        {
            if (CurrentLaserCharges >= _playerConfig.MaximumLaserCharges) return;
            _currentLaserBarFill += _playerConfig.FillPercent;
            var barPercent = OnLaserFilled?.Invoke(_currentLaserBarFill);
            if (barPercent >= 1)
            {
                CurrentLaserCharges++;
                if (CurrentLaserCharges >= _playerConfig.MaximumLaserCharges) return;
                _currentLaserBarFill = 0;
                OnLaserFilled?.Invoke(_currentLaserBarFill);
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


        private void ShootPrimary(InputAction.CallbackContext callbackContext)
        {
            var bullet = Get1();

            bullet.Project(TransformUp);
        }

        private void ShootSecondary(InputAction.CallbackContext callbackContext)
        {
            if (CurrentLaserCharges < 1) return;
            var bullet = Get2();

            ReduceCharges();
            bullet.Project(TransformUp);
        }


        private void ReduceCharges()
        {
            CurrentLaserCharges--;
            _currentLaserBarFill = 0;
        }


        public override void GetSetup1(Projectile projectile)
        {
            base.GetSetup1(projectile);
            var bulletTransform = projectile.transform;
            bulletTransform.position = transform.position;
            bulletTransform.rotation = transform.rotation;
            projectile.transform.SetParent(transform);
        }

        public override void GetSetup2(Projectile projectile)
        {
            base.GetSetup2(projectile);
            var bulletTransform = projectile.transform;
            bulletTransform.position = transform.position;
            bulletTransform.rotation = transform.rotation;
            projectile.transform.SetParent(transform);
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Asteroid>() || col.GetComponent<UFO>())
            {
                FatalCollide();
            }

            if (col.transform.parent.TryGetComponent(out Boundary boundary))
            {
                InfiniteBorder(col, boundary);
            }
        }

        private void FatalCollide()
        {
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;

            gameObject.SetActive(false);

            OnAsteroidCollided?.Invoke();
        }

        private void InfiniteBorder(Collider2D col, Boundary boundary)
        {
        
            if (col == boundary.LeftBoxCollider2D)
            {
                transform.position = new Vector3(-transform.position.x, transform.position.y,
                    transform.position.z);
            }

            if (col == boundary.UpBoxCollider2D)
            {
                transform.position = new Vector3(transform.position.x, -transform.position.y,
                    transform.position.z);
            }

            if (col == boundary.RightBoxCollider2D)
            {
                transform.position = new Vector3(-transform.position.x, transform.position.y,
                    transform.position.z);
            }

            if (col == boundary.DownBoxCollider2D)
            {
                transform.position = new Vector3(transform.position.x, -transform.position.y,
                    transform.position.z);
            }
        }

        public class Factory : PlaceholderFactory<Player>
        {
            private Player _currentPlayer;

            public Player CurrentPlayer => _currentPlayer;

            public override Player Create()
            {
                var player = base.Create();
                _currentPlayer = player;
                player.CurrentLaserCharges = 0;
                return player;
            }
        }
    }
}