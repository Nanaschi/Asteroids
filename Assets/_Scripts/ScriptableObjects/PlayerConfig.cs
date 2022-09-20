using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(PlayerConfig),
    menuName = "Scriptable Objects/" + nameof(PlayerConfig))]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private PlayerModel _playerModel;
    
    public float FillSpeed => _playerModel.FillSpeed;

    public float FillPercent => _playerModel.FillPercent;

    public int MaximumLaserCharges => _playerModel.MaximumLaserCharges;
    public int MaxLives => _playerModel.MaxLives;
    public int RespawnTime => _playerModel.RespawnTime;
    public int TimeOfInvincibility => _playerModel.TimeOfInvincibility;
    public float MovementSpeed => _playerModel.MovementSpeed;
    public float TurnSpeed => _playerModel.TurnSpeed;
}