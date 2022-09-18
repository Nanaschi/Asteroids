using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AsteroidManager : PoolerBase<Asteroid>
{
    [SerializeField] private Asteroid _asteroidPrefab;
    public Asteroid AsteroidPrefab => _asteroidPrefab;

    private GameObject _asteroidPool;

    private void Awake()
    {
        _asteroidPool = new GameObject(nameof(_asteroidPool));
        _asteroidPool.transform.SetParent(transform);
        InitPool(_asteroidPrefab);
    }

    private void OnEnable()
    {
        Asteroid.OnAsteroidDestroyed += Release;
        Asteroid.OnAsteroidSplit += Get;
    }

    private void OnDisable()
    {
        Asteroid.OnAsteroidDestroyed -= Release;
        Asteroid.OnAsteroidSplit -= Get;
    }

    private void Start()
    {
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
            Asteroid asteroid = Get();

            asteroid.transform.position = spawnPoint;
            asteroid.transform.rotation = spawnRotation;

            asteroid.transform.SetParent(_asteroidPool.transform);
            asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);

            asteroid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}