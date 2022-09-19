using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UFO : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    [SerializeField] private float moveSpeed;

    public void Construct(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position,
            moveSpeed * Time.deltaTime);
    }
}