using System;
using UnityEngine;

[Serializable]
public struct PlayerModel
{
    [SerializeField] private int _maxLives;

    [SerializeField] private int _respawnTime;

    [SerializeField] private int _timeOfInvincibility;
    [SerializeField] private float _movementSpeed; //TODO make it a SO model
    [SerializeField] private float _turnSpeed;

    public float MovementSpeed => _movementSpeed;

    public float TurnSpeed => _turnSpeed;

    public int MaxLives => _maxLives;

    public int RespawnTime => _respawnTime;

    public int TimeOfInvincibility => _timeOfInvincibility;
}