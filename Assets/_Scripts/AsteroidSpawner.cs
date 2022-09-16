using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _trajectoryVariance;
    [SerializeField] private int _spawnAmount; //TODO: add range as well as to other fields
    [SerializeField] private int _spawnDistance; //TODO: add range as well as to other fields
    [SerializeField] private Asteroid _asteroidPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), _spawnRate, _spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 spawnPoint = transform.position + spawnDirection;
            float variance = Random.Range(-_trajectoryVariance, _trajectoryVariance);

            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Asteroid asteroid = Instantiate(_asteroidPrefab, spawnPoint, spawnRotation);

            asteroid.Size = Random.Range(asteroid.MinSize, asteroid.MaxSize);
            
            asteroid.SetTrajectory(spawnRotation * - spawnDirection );
        }
    }
}