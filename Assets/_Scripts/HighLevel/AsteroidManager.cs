using _Scripts.FlyingObjects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.HighLevel
{
    public class AsteroidManager : DoublePoolerBase<Asteroid, UFO>
    {
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private UFO _ufoPrefab;
        public Asteroid AsteroidPrefab => _asteroidPrefab;

        private GameObject _asteroidPool;
        private Player.Factory _factory;


        [Inject]
        private void InitiInject(Player.Factory factory)
        {
            _factory = factory;
        }
    
        private void Awake()
        {
            _asteroidPool = new GameObject(nameof(_asteroidPool));
            _asteroidPool.transform.SetParent(transform);
            InitPool(_asteroidPrefab, _ufoPrefab);
        }

        private void OnEnable()
        {
            Asteroid.OnAsteroidDestroyed += Release1;
            Asteroid.OnAsteroidSplit += Get1;
        }

        private void OnDisable()
        {
            Asteroid.OnAsteroidDestroyed -= Release1;
            Asteroid.OnAsteroidSplit -= Get1;
        }

        private void Start()
        {
            StartSpawningAsteroids();
        }

        private void StartSpawningAsteroids()
        {
            if (_asteroidPrefab.AsteroidConfig.SpawnRate <= 0) return;
            InvokeRepeating(nameof(Spawn), _asteroidPrefab.AsteroidConfig.SpawnRate,
                _asteroidPrefab.AsteroidConfig.SpawnRate);
        }

        public void Spawn()
        {
            for (int i = 0; i < _asteroidPrefab.AsteroidConfig.SpawnAmount; i++)
            {
                Vector3 spawnDirection = Random.insideUnitCircle.normalized *
                                         _asteroidPrefab.AsteroidConfig.SpawnDistance;
                Vector3 spawnPoint = transform.position + spawnDirection;
                float variance = Random.Range(-_asteroidPrefab.AsteroidConfig.TrajectoryVariance,
                    _asteroidPrefab.AsteroidConfig.TrajectoryVariance);

                Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);
                Asteroid asteroid = Get1();

                asteroid.transform.position = spawnPoint;
                asteroid.transform.rotation = spawnRotation;

                asteroid.transform.SetParent(_asteroidPool.transform);
                asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);

                asteroid.SetTrajectory(spawnRotation * -spawnDirection);


                UFO ufo = Get2();
                ufo.Construct(_factory.CurrentPlayer);
                ufo.transform.position = spawnPoint;
                ufo.transform.rotation = spawnRotation;

                ufo.transform.SetParent(_asteroidPool.transform);
            }
        }
    }
}