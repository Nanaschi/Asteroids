using System;
using UnityEngine;

[Serializable]
public struct PlayerModel
{
    [SerializeField] private int _maxLives;

    [SerializeField] private int _respawnTime;

    [SerializeField] private int _timeOfInvincibility;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private float _fillPercent;
    [SerializeField] private int _maximumLaserCharges;

    public float FillSpeed => _fillSpeed;

    public float FillPercent => _fillPercent;

    public int MaximumLaserCharges => _maximumLaserCharges;

    public float MovementSpeed => _movementSpeed;

    public float TurnSpeed => _turnSpeed;

    public int MaxLives => _maxLives;

    public int RespawnTime => _respawnTime;

    public int TimeOfInvincibility => _timeOfInvincibility;
}