using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position,
            moveSpeed * Time.deltaTime);
    }
}