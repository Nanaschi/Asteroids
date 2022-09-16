using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private const int FullCircle = 360;

    private float _size = 1f;
    private float _minSize = .5f;
    private float _maxSize = 1.5f;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Sprite[] _sprite;

    private void Start()
    {
        _spriteRenderer.sprite = _sprite[Random.Range(0, _sprite.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * FullCircle);

        transform.localScale = Vector3.one * _size;

        _rigidbody2D.mass = _size;
    }
}
