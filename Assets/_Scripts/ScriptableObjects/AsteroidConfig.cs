using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(AsteroidConfig),
    menuName = "Scriptable Objects/" + nameof(AsteroidConfig))]
public class AsteroidConfig : ScriptableObject
{
    [SerializeField] private AsteroidModel _asteroidModel;

    [SerializeField] private Sprite[] _spriteVariations;

    public Sprite[] SpriteVariations => _spriteVariations;

    public float SpawnRate => _asteroidModel.SpawnRate;

    public float TrajectoryVariance => _asteroidModel.TrajectoryVariance;

    public int SpawnAmount => _asteroidModel.SpawnAmount;

    public int SpawnDistance => _asteroidModel.SpawnDistance;
    
    public float Speed => _asteroidModel.Speed;

    public float MinSize => _asteroidModel.MinSize;

    public float MaxSize => _asteroidModel.MaxSize;

    public float SplitCircleOffset => _asteroidModel.SplitCircleOffset;
}