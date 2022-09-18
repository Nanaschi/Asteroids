using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AsteroidManager : MonoBehaviour
{
    //TODO: add range as well as to other fields
    //TODO: add range as well as to other fields
    [SerializeField] private Asteroid _asteroidPrefab;
    public Asteroid AsteroidPrefab => _asteroidPrefab;
    

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
            //TODO: pool TODO: spawn inside a specific GO
            Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, spawnRotation);

            asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);

            asteroid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}