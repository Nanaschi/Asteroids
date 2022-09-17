using System;
using UnityEngine;

[Serializable]
internal struct AsteroidModel
{
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _trajectoryVariance;
    [SerializeField] private int _spawnAmount; //TODO: add range as well as to other fields
    [SerializeField] private int _spawnDistance; //TODO: add range as well as to other fields
    [SerializeField] private float _speed;
    [SerializeField] private float _minSize;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _splitCircleOffset;
    public float SpawnRate => _spawnRate;

    public float TrajectoryVariance => _trajectoryVariance;

    public int SpawnAmount => _spawnAmount;

    public int SpawnDistance => _spawnDistance;

    public float Speed => _speed;

    public float MinSize => _minSize;

    public float MaxSize => _maxSize;

    public float SplitCircleOffset => _splitCircleOffset;
}