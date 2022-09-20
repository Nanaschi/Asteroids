using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [Serializable]
    internal struct AsteroidModel
    {
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _trajectoryVariance;
        [SerializeField] [Range(1,10)] private int _spawnAmount;
        [SerializeField] private int _spawnDistance;
        [SerializeField] private float _speed;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _splitCircleOffset;
        [SerializeField] private float _selfDestructionTime;

        public float SelfDestructionTime => _selfDestructionTime;

        public float SpawnRate => _spawnRate;

        public float TrajectoryVariance => _trajectoryVariance;

        public int SpawnAmount => _spawnAmount;

        public int SpawnDistance => _spawnDistance;

        public float Speed => _speed;

        public float MinSize => _minSize;

        public float MaxSize => _maxSize;

        public float SplitCircleOffset => _splitCircleOffset;
    }
}