using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private const int FullCircle = 360;

    private float _size = 1f;
    [SerializeField] private float _speed;


    [SerializeField] private float _minSize = .5f;
    [SerializeField] private float _maxSize = 1.5f;
    [SerializeField] private float _splitCircleOffset;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Sprite[] _sprite;

    public static Action<Asteroid> OnAsteroidDestroyed;

    public float Size
    {
        get => _size;
        set => _size = value;
    }

    public float MinSize => _minSize;

    public float MaxSize => _maxSize;

    private void Start()
    {
        _spriteRenderer.sprite = _sprite[Random.Range(0, _sprite.Length)];

        transform.eulerAngles = new Vector3(0, 0, Random.value * FullCircle);

        transform.localScale = Vector3.one * _size;

        _rigidbody2D.mass = _size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _speed);
        Destroy(gameObject, 30f); //TODO: change to pool 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("Collided");
        if (col.GetComponent<Bullet>())
        {
            if ((_size * .5f) >= _minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            //TODO: Change to OnAsteroidDestroyed
            /*FindObjectOfType<GameManager>().AsteroidDestroyed(this);*/
            OnAsteroidDestroyed?.Invoke(this);
            Destroy(gameObject); //TODO: pool
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * _splitCircleOffset;

        Asteroid splitAsteroid = Instantiate(this, position, transform.rotation); //TODO: pool

        splitAsteroid.Size = Size * _splitCircleOffset;

        splitAsteroid.SetTrajectory(Random.insideUnitCircle.normalized * _speed);
    }
}